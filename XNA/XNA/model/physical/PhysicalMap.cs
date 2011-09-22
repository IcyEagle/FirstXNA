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
            BLOCKS_IN_ROW = (int)Math.Ceiling((float)Game1.SCREEN_WIDTH / Terrain.BLOCK_SIZE);
            BLOCKS_IN_COLUMN = (int)Math.Ceiling((float)Game1.SCREEN_HEIGHT / Terrain.BLOCK_SIZE);
        }

        internal PhysicalMap()
        {
            this.map = new int[BLOCKS_IN_ROW, BLOCKS_IN_COLUMN];
        }

        /**
         * Change blocks state in a specified range.
         */
        public void changeRange(Vector2 leftTop, Vector2 rightBottom, State state)
        {
            fixRanges(ref leftTop, ref rightBottom);

            for (int x = (int)leftTop.X; x < (int)rightBottom.X; ++x)
            {
                for (int y = (int)leftTop.Y; y < (int)rightBottom.Y; ++y)
                {
                    map[x, y] += (int)state;

                    if (blockExists(x, y))
                    {
                        if (readyToDeactivate(x, y, state))
                        {
                            getBlock(x, y).disablePhysics();
                        }
                        else if (readyToActivate(x, y, state))
                        {
                            getBlock(x, y).enablePhysics();
                        }
                    }
                }
            }
        }

        /**
         * Checks and fixes physical boundaries.
         */
        private void fixRanges(ref Vector2 leftTop, ref Vector2 rightBottom)
        {
            if (leftTop.X < 0) leftTop.X = 0;
            if (leftTop.Y < 0) leftTop.Y = 0;
            if (rightBottom.X > BLOCKS_IN_ROW) rightBottom.X = BLOCKS_IN_ROW;
            if (rightBottom.Y > BLOCKS_IN_COLUMN) rightBottom.Y = BLOCKS_IN_COLUMN;
        }

        /**
         * Check whether corresponding block exists.
         */
        private bool blockExists(int x, int y)
        {
            return GameModel.instance.terrain.map[x, y] != null;
        }

        /**
         * Return corresponding block.
         */
        private Block getBlock(int x, int y)
        {
            return GameModel.instance.terrain.map[x, y];
        }

        /**
         * Check whether block physics should be activated.
         */
        private bool readyToActivate(int x, int y, State state)
        {
            return state == State.INCREASE && map[x, y] == 1;
        }

        /**
         * Check whether block physics should be diactivated.
         */
        private bool readyToDeactivate(int x, int y, State state)
        {
            return state == State.DECREASE && map[x, y] == 0;
        }
    }
}
