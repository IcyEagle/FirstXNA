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
        // DEBUG
        public static Texture2D enabledTexture;

        public delegate void onDestroyHandler (Block block);
        public event onDestroyHandler onDestroy;

        private Texture2D texture;
        public int width;
        public int height;
        public int x;
        public int y;
        private Body body;

        public Block(Texture2D texture, Rectangle rectangle) {
            this.texture = texture;
            this.width = rectangle.Width;
            this.height = rectangle.Height;
            this.x = rectangle.X;
            this.y = rectangle.Y;
        }

        public void Draw(SpriteBatch batch) {
            //batch.Draw(this.texture, new Rectangle(x, y, width, height), Color.White);

            //DEBUG
            batch.Draw(body != null ? Block.enabledTexture : this.texture, new Rectangle(x, y, width, height), Color.White);
        }
        
        public void enablePhysics()
        {
            if (this.body == null)
            {
                this.body = GameModel.instance.bodyManager.createBlockBody(this);
            }
        }

        public void disablePhysics()
        {
            if (body != null)
            {
                GameModel.instance.bodyManager.removeBody(body);
                body = null;
            }
        }

        public bool damage()
        {
            // change logic later.
            bool destroyed = true;

            if (destroyed)
            {
                onDestroy.Invoke(this);
            }

            return destroyed;
        }

    }

    
}
