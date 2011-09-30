using System;
using Microsoft.Xna.Framework;
using XNA.model.block;

namespace XNA.model.physical
{
    class PhysicalMap
    {
        private readonly int[,] _map;

        internal static readonly int BlocksInRow;
        internal static readonly int BlocksInColumn;

        internal enum State {
            INCREASE = 1,
            DECREASE = -1
        }

        static PhysicalMap()
        {
            BlocksInRow = (int)Math.Ceiling((float)Game1.ScreenWidth / Terrain.BLOCK_SIZE);
            BlocksInColumn = (int)Math.Ceiling((float)Game1.ScreenHeight / Terrain.BLOCK_SIZE);
        }

        internal PhysicalMap()
        {
            _map = new int[BlocksInRow, BlocksInColumn];
        }

        /**
         * Change blocks state in a specified range.
         */
        public void ChangeRange(Point leftTop, Point rightBottom, State state)
        {
            fixRanges(ref leftTop, ref rightBottom);

            for (var x = leftTop.X; x < rightBottom.X; ++x)
            {
                for (var y = leftTop.Y; y < rightBottom.Y; ++y)
                {
                    _map[x, y] += (int)state;

                    if (blockExists(x, y))
                    {
                        if (ReadyToDeactivate(x, y, state))
                        {
                            getBlock(x, y).disablePhysics();
                        }
                        else if (ReadyToActivate(x, y, state))
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
        private void fixRanges(ref Point leftTop, ref Point rightBottom)
        {
            if (leftTop.X < 0) leftTop.X = 0;
            if (leftTop.Y < 0) leftTop.Y = 0;
            if (rightBottom.X > BlocksInRow) rightBottom.X = BlocksInRow;
            if (rightBottom.Y > BlocksInColumn) rightBottom.Y = BlocksInColumn;
        }

        /**
         * Check whether corresponding block exists.
         */
        private bool blockExists(int x, int y)
        {
            return GameModel.Instance.Terrain.map[x, y] != null;
        }

        /**
         * Return corresponding block.
         */
        private Block getBlock(int x, int y)
        {
            return GameModel.Instance.Terrain.map[x, y];
        }

        /**
         * Check whether block physics should be activated.
         */
        private bool ReadyToActivate(int x, int y, State state)
        {
            return state == State.INCREASE && _map[x, y] == 1;
        }

        /**
         * Check whether block physics should be diactivated.
         */
        private bool ReadyToDeactivate(int x, int y, State state)
        {
            return state == State.DECREASE && _map[x, y] == 0;
        }
    }
}
