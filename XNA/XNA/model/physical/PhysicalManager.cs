using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA.model.grid;

namespace XNA.model.physical
{
    class PhysicalManager
    {
        private static readonly float BLOCKS_PER_REGION;

        private PhysicalMap physicalMap;

        static PhysicalManager()
        {
            BLOCKS_PER_REGION = (float)Grid.REGION_SIZE / Terrain.BLOCK_SIZE;
        }

        public PhysicalManager()
        {
            this.physicalMap = new PhysicalMap();

            GameModel.instance.grid.onChangeRegion += new Grid.onChangeRegionDelegate(onChangeRegionHandler);
        }

        private void onChangeRegionHandler(Object target, Vector2 source, Vector2 destination)
        {
            // perhaps, it's not optimized algorithm.

            // disable blocks.
            Vector2 disableLeftTop = new Vector2((float)Math.Floor((source.X - 1) * BLOCKS_PER_REGION), (float)Math.Floor((source.Y - 1) * BLOCKS_PER_REGION));
            Vector2 disableRightBottom = new Vector2((float)Math.Floor((source.X - 1) * BLOCKS_PER_REGION), (float)Math.Floor((source.Y + 1) * BLOCKS_PER_REGION));
            physicalMap.changeRange(disableLeftTop, disableRightBottom, PhysicalMap.State.DECREASE);

            // enable blocks.
            Vector2 enableLeftTop = new Vector2((float)Math.Floor((destination.X - 1) * BLOCKS_PER_REGION), (float)Math.Floor((destination.Y - 1) * BLOCKS_PER_REGION));
            Vector2 enableRightBottom = new Vector2((float)Math.Floor((destination.X - 1) * BLOCKS_PER_REGION), (float)Math.Floor((destination.Y + 1) * BLOCKS_PER_REGION));
            physicalMap.changeRange(enableLeftTop, enableRightBottom, PhysicalMap.State.INCREASE);
        }
    }
}
