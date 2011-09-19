using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNA.model.physical
{
    class PhysicalMap
    {
        private int[,] map;

        internal static readonly int BLOCKS_IN_ROW;
        internal static readonly int BLOCKS_IN_COLUMN;

        internal enum State {
            INCREASE = 1,
            DECREASE = -1
        }

        static PhysicalMap()
        {
            BLOCKS_IN_ROW = Game1.SCREEN_WIDTH / Terrain.BLOCK_SIZE;
            BLOCKS_IN_COLUMN = Game1.SCREEN_HEIGHT / Terrain.BLOCK_SIZE;
        }

        internal PhysicalMap()
        {
            this.map = new int[BLOCKS_IN_ROW, BLOCKS_IN_COLUMN];
        }

        public void changeRange(Vector2 leftTop, Vector2 rightBottom, State state)
        {
            // currect out of range values.
            if (leftTop.X < 0) leftTop.X = 0;
            if (leftTop.Y < 0) leftTop.Y = 0;
            if (rightBottom.X > BLOCKS_IN_ROW) rightBottom.X = BLOCKS_IN_ROW;
            if (rightBottom.Y > BLOCKS_IN_COLUMN) rightBottom.Y = BLOCKS_IN_COLUMN;

            for (int x = (int)leftTop.X; x < (int)rightBottom.X; ++x)
            {
                for (int y = (int)leftTop.Y; x < (int)rightBottom.Y; ++y)
                {
                    map[x, y] += (int)state;

                    if (state == State.DECREASE && map[x, y] == 0)
                    {
                        GameModel.instance.terrain.map[x, y].disablePhysics();
                    }

                    if (state == State.INCREASE && map[x, y] == 1)
                    {
                        GameModel.instance.terrain.map[x, y].enablePhysics();
                    }
                }
            }
        }
    }
}
