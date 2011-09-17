using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNA.model
{
    class ItemManager
    {
        private Game1 game;

        public enum ItemType {
            BLOCK_GENERIC
        }

        public ItemManager(Game1 game)
        {
            this.game = game;
        }

        public Item getItem(ItemType type) {
            TextureHelper helper = (TextureHelper)game.Services.GetService(typeof(TextureHelper));

            int width = 15;
            int height = 15;

            Texture2D texture = helper.generateSimpleTexture(width, height, Color.Magenta);

            return new Item(texture, width, height);
        }
    }
}
