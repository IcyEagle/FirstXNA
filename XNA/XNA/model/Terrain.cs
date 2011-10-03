using System.Collections.Generic;

using Microsoft.Xna.Framework;
using XNA.model.block;
using XNA.model.item;

namespace XNA.model
{
    class Terrain
    {
        public static int BLOCK_SIZE = 16;

        public Block[,] map;

        // TEMP.
        public List<Item> items = new List<Item>();

        public Terrain(Block[,] map)
        {
            this.map = map;

            GameModel.Instance.MouseInput.onClick += new MouseInput.onClickDelegate(onClickHandler);

            init();
        }

        public void onClickHandler(MouseInput.OnClickArgs args)
        {
            Vector2 coordinates = MouseInput.toAbsolute(new Vector2(args.state.X, args.state.Y));
            Point blockPosition = calculateBlockPositionByCoordinate(coordinates);

            if (blockPosition.X < 0 || blockPosition.Y < 0 || blockPosition.X >= map.GetLength(0) || blockPosition.Y >= map.GetLength(1))
            {
                return;
            }

            Block block = map[blockPosition.X, blockPosition.Y];

            // damage block.
            if (block != null)
            {
                bool destroyed = block.damage();
                if (destroyed)
                {
                    map[blockPosition.X, blockPosition.Y] = null;
                }
            }
        }

        private Point calculateBlockPositionByCoordinate(Vector2 position)
        {
            //return new Point((int)((position.X + BLOCK_SIZE / 2) / BLOCK_SIZE), (int)((position.Y + BLOCK_SIZE / 2) / BLOCK_SIZE));
            return new Point((int)((position.X + BLOCK_SIZE / 2) / BLOCK_SIZE), (int)(position.Y / BLOCK_SIZE) + 1);
        }

        protected void init()
        {
            foreach (Block block in map)
            {
                if (block != null)
                {
                    block.onDestroy += new Block.onDestroyHandler(onBlockDestroyHandler);
                }
            }
        }

        protected void onBlockDestroyHandler(Block block)
        {
            block.disablePhysics();
            GameModel.Instance.DrawManager.RemoveObjectForDraw(block);
            Item item = GameModel.Instance.GenericFactory.CreateItem(block.Position.X, block.Position.Y);
            item.ThrowOut();
            items.Add(item);
        }
    }
}
