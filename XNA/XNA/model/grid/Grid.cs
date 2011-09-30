using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XNA.model.grid
{
    public class Grid
    {
        public const int RegionSize = 64; // pixels
        public static readonly int RegionsInRow;
        public static readonly int RegionsInColumn;

        // Deprecated.
        public delegate void OnEnterRegionDelegate(ActiveObject target, Point destination);
        public event OnEnterRegionDelegate OnEnterRegion;

        // Deprecated.
        public delegate void OnLeaveRegionDelegate(ActiveObject target, Point source);
        public event OnLeaveRegionDelegate OnLeaveRegion;

        public delegate void OnChangeRegionDelegate(ActiveObject target, Point source, Point destination);
        public event OnChangeRegionDelegate OnChangeRegion;

        private readonly Region[,] _regions;

        // provides information about object position (into region).
        private readonly Dictionary<ActiveObject, Point> _objectMap = new Dictionary<ActiveObject, Point>();

        static Grid()
        {
            RegionsInRow = (int)Math.Ceiling((double)Game1.ScreenWidth / RegionSize);
            RegionsInColumn = (int)Math.Ceiling((double)Game1.ScreenHeight / RegionSize);
        }

        public Grid()
        {
            _regions = new Region[RegionsInRow, RegionsInColumn];

            for (int i = 0; i < RegionsInRow; ++i)
            {
                for (int j = 0; j < RegionsInColumn; ++j)
                {
                    _regions[i, j] = new Region();
                }
            }
        }

        public bool HasRegionByCoordinate(Point region)
        {
            return ValidateRegion(region);
        }

        public Region GetRegion(Point position)
        {
            if ( ! ValidateRegion(position))
            {
                throw new Exception("Out of region range");
            }

            return _regions[position.X, position.Y];
        }

        /**
         * Update object position on grid.
         */
        public void MoveTo(ActiveObject target, Vector2 coordinates)
        {
            Point destinationRegion = DetermineRegion(coordinates);

            // assert.
            if (!ValidateRegion(destinationRegion))
            {
                return;
            }

            if (IsOnMap(target))
            {
                Point sourceRegion = GetCurrentRegion(target);
                if (sourceRegion != destinationRegion)
                {
                    Replace(target, sourceRegion, destinationRegion);
                }
            }
            else
            {
                Put(target, destinationRegion);
            }
        }

        /**
         * Remove object from region map.
         */
        public void Remove(ActiveObject target)
        {
            Point sourceRegion = GetCurrentRegion(target);
            Drop(target, sourceRegion);
        }

        /**
         * Replace already existed object from one region to another.
         */
        private void Replace(ActiveObject target, Point source, Point destination)
        {
            _regions[source.X, source.Y].RemoveMember(target);
            _regions[destination.X, destination.Y].AddMember(target);
            _objectMap[target] = destination;
            OnChangeRegion(target, source, destination);
        }

        /**
         * Create new object on region map.
         */
        private void Put(ActiveObject target, Point destination)
        {
            _regions[destination.X, destination.Y].AddMember(target);
            _objectMap.Add(target, destination);
            OnEnterRegion(target, destination);
        }

        /**
         * Delete object from region map.
         */
        private void Drop(ActiveObject target, Point source)
        {
            _regions[source.X, source.Y].RemoveMember(target);
            _objectMap.Remove(target);
            OnLeaveRegion(target, source);
        }

        /**
         * Calculates region position by world absolute coordinates.
         */
        private Point DetermineRegion(Vector2 coordinates)
        {
            return new Point((int)Math.Floor(coordinates.X / RegionSize), (int)Math.Floor(coordinates.Y / RegionSize));
        }

        /**
         * This assert method.
         * It shouldn't be used in production version.
         */
        private bool ValidateRegion(Point region)
        {
            return region.X >= 0 && region.X < RegionsInRow && region.Y >= 0 && region.Y < RegionsInColumn;
        }

        /**
         * Checks whether object already placed on map.
         */
        private bool IsOnMap(ActiveObject target)
        {
            return _objectMap.ContainsKey(target);
        }

        /**
         * Returns object current region.
         */
        private Point GetCurrentRegion(ActiveObject target)
        {
            return _objectMap[target];
        }
    }
}
