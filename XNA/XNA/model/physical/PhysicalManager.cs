using System;
using Microsoft.Xna.Framework;
using XNA.model.grid;

namespace XNA.model.physical
{
    class PhysicalManager
    {
        private static readonly float BlocksPerRegion = (float) Grid.REGION_SIZE / Terrain.BLOCK_SIZE;

        private PhysicalMap physicalMap;

        static PhysicalManager()
        {
            
        }

        public PhysicalManager()
        {
            this.physicalMap = new PhysicalMap();

            GameModel.Instance.Grid.onEnterRegion += new Grid.onEnterRegionDelegate(onEnterRegionHandler);
            GameModel.Instance.Grid.onLeaveRegion += new Grid.onLeaveRegionDelegate(onLeaveRegionHandler);
        }

        private void onEnterRegionHandler(ActiveObject target, Vector2 destination)
        {
            float range = BlocksPerRegion;

            // enable blocks.
            Vector2 leftTop = new Vector2((float)Math.Floor((destination.X - 1) * range), (float)Math.Floor((destination.Y - 1) * range));
            Vector2 rightBottom = new Vector2((float)Math.Floor((destination.X + 2) * range), (float)Math.Floor((destination.Y + 2) * range));
            physicalMap.changeRange(leftTop, rightBottom, PhysicalMap.State.INCREASE);
        }

        private void onLeaveRegionHandler(ActiveObject target, Vector2 source)
        {
            float range = BlocksPerRegion;

            // disable blocks.
            Vector2 leftTop = new Vector2((float)Math.Floor((source.X - 1) * range), (float)Math.Floor((source.Y - 1) * range));
            Vector2 rightBottom = new Vector2((float)Math.Floor((source.X + 2) * range), (float)Math.Floor((source.Y + 2) * range));
            physicalMap.changeRange(leftTop, rightBottom, PhysicalMap.State.DECREASE);
        }

    }
}
