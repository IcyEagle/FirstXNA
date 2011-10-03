using System.Collections.Generic;

using Microsoft.Xna.Framework;
using XNA.model.block;
using XNA.model.input;
using XNA.model.item;

namespace XNA.model
{
    class Terrain
    {
        public static int BlockSize = 16;

        public Block[,] Map;

        // TEMP.
        public List<Item> Items = new List<Item>();

        public Terrain(Block[,] map)
        {
            Map = map;

            GameModel.Instance.MouseInput.OnClick += onClickHandler;

            Init();
        }

        public void onClickHandler(MouseInput.OnClickArgs args)
        {
            Vector2 coordinates = MouseInput.ToAbsolute(new Vector2(args.State.X, args.State.Y));
            Point blockPosition = calculateBlockPositionByCoordinate(coordinates);

            if (blockPosition.X < 0 || blockPosition.Y < 0 || blockPosition.X >= Map.GetLength(0) || blockPosition.Y >= Map.GetLength(1))
            {
                return;
            }

            Block block = Map[blockPosition.X, blockPosition.Y];

            // damage block.
            if (block != null)
            {
                bool destroyed = block.damage();
                if (destroyed)
                {
                    Map[blockPosition.X, blockPosition.Y] = null;
                }
            }
        }

        private Point calculateBlockPositionByCoordinate(Vector2 position)
        {
            return new Point((int)((position.X + BlockSize / 2) / BlockSize), (int)((position.Y + BlockSize / 2) / BlockSize));
        }

        protected void Init()
        {
            foreach (Block block in Map)
            {
                if (block != null)
                {
                    block.onDestroy += onBlockDestroyHandler;
                }
            }
        }

        protected void onBlockDestroyHandler(Block block)
        {
            block.disablePhysics();
            GameModel.Instance.DrawManager.RemoveObjectForDraw(block);
            Item item = GameModel.Instance.GenericFactory.CreateItem(block.Position.X, block.Position.Y);
            item.ThrowOut();
            Items.Add(item);
        }
    }
}
