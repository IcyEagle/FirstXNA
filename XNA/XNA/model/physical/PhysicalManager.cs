using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNA.model.grid;

namespace XNA.model.physical
{
    class PhysicalManager
    {

        private static readonly float BlocksPerRegion = (float) Grid.RegionSize / Terrain.BLOCK_SIZE;

        private readonly PhysicalMap _physicalMap = new PhysicalMap();

        static PhysicalManager()
        {

        }

        public PhysicalManager()
        {
            GameModel.Instance.Grid.OnEnterRegion += onEnterRegionHandler;
            GameModel.Instance.Grid.OnLeaveRegion += onLeaveRegionHandler;
            GameModel.Instance.Grid.OnChangeRegion += onChangeRegionHandler;
        }

        private void onEnterRegionHandler(ActiveObject target, Point destination)
        {
            IEnumerable<Point> enterRegions = Region.GetRegionRectangle(new Rectangle(destination.X - 1, destination.Y - 1, 3, 3));

            // enable blocks.
            foreach (Point enterRegion in enterRegions)
            {
                EnableRegion(enterRegion);
            }
        }

        private void onLeaveRegionHandler(ActiveObject target, Point source)
        {
            IEnumerable<Point> leaveRegions = Region.GetRegionRectangle(new Rectangle(source.X - 1, source.Y - 1, 3, 3));

            // disable blocks.
            foreach (Point leaveRegion in leaveRegions)
            {
                DisableRegion(leaveRegion);
            }
        }

        private void onChangeRegionHandler(ActiveObject target, Point source, Point destination)
        {
            IEnumerable<Point> enterRegions = Region.GetRegionRectangleDifference(new Rectangle(destination.X - 1, destination.Y - 1, 3, 3), new Rectangle(source.X - 1, source.Y - 1, 3, 3));

            // enable blocks.
            foreach (Point enterRegion in enterRegions)
            {
                EnableRegion(enterRegion);
            }

            IEnumerable<Point> leaveRegions = Region.GetRegionRectangleDifference(new Rectangle(source.X - 1, source.Y - 1, 3, 3), new Rectangle(destination.X - 1, destination.Y - 1, 3, 3));

            // disable blocks.
            foreach (Point leaveRegion in leaveRegions)
            {
                DisableRegion(leaveRegion);
            }

        }

        private void EnableRegion(Point region)
        {
            float range = BlocksPerRegion;

            var leftTop = new Point((int)Math.Floor((region.X) * range), (int)Math.Floor((region.Y) * range));
            var rightBottom = new Point((int)Math.Floor((region.X + 1) * range), (int)Math.Floor((region.Y + 1) * range));
            _physicalMap.ChangeRange(leftTop, rightBottom, PhysicalMap.State.INCREASE);
        }

        private void DisableRegion(Point region)
        {
            float range = BlocksPerRegion;

            var leftTop = new Point((int)Math.Floor((region.X) * range), (int)Math.Floor((region.Y) * range));
            var rightBottom = new Point((int)Math.Floor((region.X + 1) * range), (int)Math.Floor((region.Y + 1) * range));
            _physicalMap.ChangeRange(leftTop, rightBottom, PhysicalMap.State.DECREASE);
        }
    }
}
