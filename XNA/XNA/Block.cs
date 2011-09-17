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

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

namespace XNA.model
{
    class Block
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Body body;

        public Block(Texture2D texture, Rectangle rectangle) {
            this.texture = texture;
            this.rectangle = rectangle;
        }

        public void Draw(SpriteBatch batch) {
            batch.Draw(this.texture, new Rectangle(rectangle.X, rectangle.Y, texture.Width, texture.Height), Color.White);
        }
    }
}
