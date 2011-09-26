using System;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using XNA.model.grid;

namespace XNA.model.item
{
    class Item : ActiveObject
    {
        /*public int width;
        public int height;

        private Texture2D texture;
        private Body body;
        private ActiveObject moveable;

        public int x { get { return (int) body.Position.X; } }
        public int y { get { return (int) body.Position.Y; } }*/

        public Item(Texture2D texture, int width, int height, Body body)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.body = body;

            //this.moveable = new ActiveObject(this);
        }

        public Item(Texture2D texture, int width, int height, Body body, Vector2 position)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.body = body;
            setPosition(position.X, position.Y);
        }

        /*public void setPosition(float x, float y)
        {
            body.Position = new Vector2(x, y);
        }*/

        public void throwOut()
        {
            Random rand = new Random();
            //body.LinearVelocity -= new Vector2(rand.Next(10, 30), rand.Next(30, 50));
            body.LinearVelocity = new Vector2(rand.Next(-50, 50), rand.Next(-75, -50));
        }

        public void approachTo(Vector2 anchor)
        {
            Vector2 diff = body.Position - anchor;
            body.LinearVelocity -= new Vector2(Math.Sign(diff.X) * 100, Math.Sign(diff.Y) * 100);
        }

        /*public void Draw(SpriteBatch batch)
        {
            batch.Draw(this.texture, new Rectangle(x, y, width, height), null, Color.White, body.Rotation, new Vector2(width / 2, height / 2), SpriteEffects.None, 0f);
        }

        public void Update()
        {
            //moveable.UpdatePosition(body.Position);
        }*/
    }
}
