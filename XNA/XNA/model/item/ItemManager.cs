using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace XNA.model
{
    class ItemManager
    {
        public enum ItemType {
            BLOCK_GENERIC
        }

        public Item getItem(ItemType type) {
            TextureHelper helper = (TextureHelper)GameModel.instance.game.Services.GetService(typeof(TextureHelper));

            int width = 16;
            int height = 16;

            Texture2D texture = helper.generateSimpleTexture(width, height, Color.Magenta);
            Body body = GameModel.instance.bodyManager.createPickableBody(width, height);

            return new Item(texture, width, height, body);
        }
    }
}
