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
        public int width;
        public int height;

        private Texture2D texture;
        private Body body;

        public int x { get { return (int) body.Position.X; } }
        public int y { get { return (int) body.Position.Y; } }

        public Item(Texture2D texture, int width, int height, Body body)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.body = body;
        }

        public Item(Texture2D texture, int width, int height, Body body, Vector2 position)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.body = body;
            this.body.Position = position;
        }

        public void setPosition(float x, float y)
        {
            body.Position = new Vector2(x + width / 2, y + height / 2);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(this.texture, new Rectangle(x, y, width, height), Color.White);
        }
    }
}
