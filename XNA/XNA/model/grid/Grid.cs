using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XNA.model.grid
{
    public class Grid
    {
        public const int REGION_SIZE = 64; // pixels
        public static readonly int REGIONS_IN_ROW;
        public static readonly int REGIONS_IN_COLUMN;

        public delegate void onEnterRegionDelegate(ActiveObject target, Vector2 destination);
        public event onEnterRegionDelegate onEnterRegion;

        public delegate void onLeaveRegionDelegate(ActiveObject target, Vector2 source);
        public event onLeaveRegionDelegate onLeaveRegion;

        private Region[,] regions;

        // provides information about object position (into region).
        private Dictionary<ActiveObject, Vector2> objectMap;

        static Grid()
        {
            REGIONS_IN_ROW = (int)Math.Ceiling((double)Game1.SCREEN_WIDTH / REGION_SIZE);
            REGIONS_IN_COLUMN = (int)Math.Ceiling((double)Game1.SCREEN_HEIGHT / REGION_SIZE);
        }

        public Grid()
        {
            this.regions = new Region[REGIONS_IN_ROW, REGIONS_IN_COLUMN];
            this.objectMap = new Dictionary<ActiveObject, Vector2>();

            for (int i = 0; i < REGIONS_IN_ROW; ++i)
            {
                for (int j = 0; j < REGIONS_IN_COLUMN; ++j)
                {
                    this.regions[i, j] = new Region();
                }
            }
        }

        public bool hasRegionByCoordinate(Vector2 region)
        {
            return validateRegion(region);
        }

        public Region getRegion(Vector2 position)
        {
            if ( ! validateRegion(position))
            {
                throw new Exception("Out of region range");
            }

            return regions[(int)position.X, (int)position.Y];
        }

        /**
         * Update object position on grid.
         */
        public void moveTo(ActiveObject target, Vector2 coordinates)
        {
            Vector2 destinationRegion = determineRegion(coordinates);

            // assert.
            if (!validateRegion(destinationRegion))
            {
                return;
            }

            if (isOnMap(target))
            {
                Vector2 sourceRegion = getCurrentRegion(target);
                if (sourceRegion != destinationRegion)
                {
                    replace(target, sourceRegion, destinationRegion);
                }
            }
            else
            {
                put(target, destinationRegion);
            }
        }

        /**
         * Remove object from region map.
         */
        public void remove(ActiveObject target)
        {
            Vector2 sourceRegion = getCurrentRegion(target);
            drop(target, sourceRegion);
        }

        /**
         * Replace already existed object from one region to another.
         */
        private void replace(ActiveObject target, Vector2 source, Vector2 destination)
        {
            regions[(int)source.X, (int)source.Y].members.Remove(target);
            regions[(int)destination.X, (int)destination.Y].members.Add(target);
            objectMap[target] = destination;
            onEnterRegion(target, destination);
            onLeaveRegion(target, source);
        }

        /**
         * Create new object on region map.
         */
        private void put(ActiveObject target, Vector2 destination)
        {
            regions[(int)destination.X, (int)destination.Y].members.Add(target);
            objectMap.Add(target, destination);
            onEnterRegion(target, destination);
        }

        /**
         * Delete object from region map.
         */
        private void drop(ActiveObject target, Vector2 source)
        {
            regions[(int)source.X, (int)source.Y].members.Remove(target);
            objectMap.Remove(target);
            onLeaveRegion(target, source);
        }

        /**
         * Calculates region position by world absolute coordinates.
         */
        private Vector2 determineRegion(Vector2 coordinates)
        {
            return new Vector2((float)Math.Floor(coordinates.X / REGION_SIZE), (float)Math.Floor(coordinates.Y / REGION_SIZE));
        }

        /**
         * This assert method.
         * It shouldn't be used in production version.
         */
        private bool validateRegion(Vector2 region)
        {
            return region.X >= 0 && region.X < REGIONS_IN_ROW && region.Y >= 0 && region.Y < REGIONS_IN_COLUMN;
        }

        /**
         * Checks whether object already placed on map.
         */
        private bool isOnMap(ActiveObject target)
        {
            return objectMap.ContainsKey(target);
        }

        /**
         * Returns object current region.
         */
        private Vector2 getCurrentRegion(ActiveObject target)
        {
            return objectMap[target];
        }
    }
}
