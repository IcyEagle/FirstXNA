using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using XNA.model;

namespace XNA.model
{
    class Terrain : DrawableGameComponent
    {
        public static int BLOCK_SIZE = 16;

        public Block[,] map;

        // TEMP.
        List<Item> items = new List<Item>();

        public Terrain(Block[,] map) : base(GameModel.instance.game)
        {
            this.map = map;

            GameModel.instance.mouseInput.onClick += new MouseInput.onClickDelegate(onClickHandler);

            init();
        }

        public void onClickHandler(MouseInput.OnClickArgs args)
        {
            Vector2 coordinates = model.MouseInput.toAbsolute(new Vector2(args.state.X, args.state.Y));
            Vector2 blockPosition = calculateBlockPositionByCoordinate(coordinates);

            if (blockPosition.X < 0 || blockPosition.Y < 0 || blockPosition.X > map.GetUpperBound(1) || blockPosition.Y > map.GetUpperBound(1))
            {
                return;
            }

            Block block = map[(int)blockPosition.X, (int)blockPosition.Y];

            // damage block.
            if (block != null)
            {
                bool destroyed = block.damage();
                if (destroyed)
                {
                    map[(int)blockPosition.X, (int)blockPosition.Y] = null;
                }
            }
        }

        private Vector2 calculateBlockPositionByCoordinate(Vector2 position)
        {
            return new Vector2(position.X / BLOCK_SIZE, position.Y / BLOCK_SIZE);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Block block in map)
            {
                if (block != null)
                {
                    block.Draw(GameModel.instance.spriteBatch);
                }
            }

            foreach (Item item in items)
            {
                item.Draw(GameModel.instance.spriteBatch);
            }
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

        public override void Update(GameTime gameTime)
        {
            foreach (Item item in items)
            {
                item.Update();
            }
        }

        protected void onBlockDestroyHandler(Block block)
        {
            block.disablePhysics();
            Item item = GameModel.instance.itemManager.getItem(ItemManager.ItemType.BLOCK_GENERIC);
            Random rand = new Random();
            float offsetX = rand.Next(30, 170) / 100f;
            float offsetY = rand.Next(30, 170) / 100f;
            item.setPosition(block.x + block.width * offsetX / 2f, block.y + block.height * offsetY / 2f);
            item.throwOut();
            items.Add(item);
        }
    }
}
