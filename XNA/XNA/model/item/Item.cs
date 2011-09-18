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
        public delegate void onCharacterNearDelegate(OnCharacterNearArgs args);
        public event onCharacterNearDelegate onCharacterNear;
        public class OnCharacterNearArgs
        {
            public Character character;
            public OnCharacterNearArgs(Character character)
            {
                this.character = character;
            }
        }

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

            onCharacterNear += new onCharacterNearDelegate(onCharacterNearHandler);
        }

        public Item(Texture2D texture, int width, int height, Body body, Vector2 position)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.body = body;
            setPosition(position.X, position.Y);
        }

        public void setPosition(float x, float y)
        {
            body.Position = new Vector2(x, y);
        }

        public void throwOut()
        {
            Random rand = new Random();
            body.LinearVelocity -= new Vector2(rand.Next(10, 30), rand.Next(30, 50));
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(this.texture, new Rectangle(x, y, width, height), null, Color.White, body.Rotation, new Vector2(width / 2, height / 2), SpriteEffects.None, 0f);
        }

        private void onCharacterNearHandler(OnCharacterNearArgs args)
        {
            System.Console.WriteLine("Character is NEAR!");
        }
    }
}
