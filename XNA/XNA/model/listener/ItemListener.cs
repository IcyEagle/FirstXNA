using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNA.model.grid;

namespace XNA.model.listener
{
    class ItemListener
    {
        public ItemListener()
        {
            GameModel.Instance.Grid.OnEnterRegion += onEnterRegionHandler;
            GameModel.Instance.Grid.OnLeaveRegion += onLeaveRegionHandler;
            GameModel.Instance.Grid.OnChangeRegion += onChangeRegionHandler;
        }

        private void onEnterRegionHandler(ActiveObject caller, Point destination)
        {
            for (int x = destination.X - 1; x < destination.X + 2; ++x)
            {
                for (int y = destination.Y - 1; y < destination.Y + 2; ++y)
                {
                    if (GameModel.Instance.Grid.HasRegionByCoordinate(new Point(x, y)))
                    {
                        Region region = GameModel.Instance.Grid.GetRegion(new Point(x, y));
                        region.ActivateMembers(caller);
                    }
                }
            }
        }

        private void onLeaveRegionHandler(ActiveObject caller, Point source)
        {
            for (int x = source.X - 1; x < source.X + 2; ++x)
            {
                for (int y = source.Y - 1; y < source.Y + 2; ++y)
                {
                    if (GameModel.Instance.Grid.HasRegionByCoordinate(new Point(x, y)))
                    {
                        Region region = GameModel.Instance.Grid.GetRegion(new Point(x, y));
                        region.DeactivateMembers(caller);
                    }
                }
            }
        }

        private void onChangeRegionHandler(ActiveObject target, Point source, Point destination)
        {
            IEnumerable<Point> enterRegions = Region.GetRegionRectangleDifference(new Rectangle(destination.X - 1, destination.Y - 1, 3, 3), new Rectangle(source.X - 1, source.Y - 1, 3, 3));

            // enable blocks.
            foreach (Point enterRegion in enterRegions)
            {
                if (GameModel.Instance.Grid.HasRegionByCoordinate(enterRegion))
                {
                    Region region = GameModel.Instance.Grid.GetRegion(enterRegion);
                    region.ActivateMembers(target);
                }
            }

            IEnumerable<Point> leaveRegions = Region.GetRegionRectangleDifference(new Rectangle(source.X - 1, source.Y - 1, 3, 3), new Rectangle(destination.X - 1, destination.Y - 1, 3, 3));

            // disable blocks.
            foreach (Point leaveRegion in leaveRegions)
            {
                if (GameModel.Instance.Grid.HasRegionByCoordinate(leaveRegion))
                {
                    Region region = GameModel.Instance.Grid.GetRegion(leaveRegion);
                    region.DeactivateMembers(target);
                }

            }

        }
    }
}
