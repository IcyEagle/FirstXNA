using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace XNA.model
{
    class Item
    {
        private Texture2D texture;
        public int width;
        public int height;
        public Vector2 position;
        private Body body;

        public Item(Texture2D texture, int width, int height)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
        }

        public Item(Texture2D texture, int width, int height, Vector2 position)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
        }

        public void setPosition(Vector2 position) {
            this.position = position;
        }

        public void Draw(SpriteBatch batch)
        {
            if (position != null)
            {
                batch.Draw(this.texture, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
            }
            else
            {
                throw new Exception("Item is not placed");
            }
        }
    }
}
