using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNA.model.input;
using Microsoft.Xna.Framework.Input;
using XNA.model.character;
using XNA.model.grid;

namespace XNA.model
{
    class Character : DrawableGameComponent
    {
        public string name;
        public int level;

        public int width;
        public int height;

        public Body body;
        public Texture2D texture;
        private ActiveObject moveable;

        private Bag bag;

        public Character(string name, int level) : base(GameModel.instance.game)
        {
            this.name = name;
            this.level = level;
            this.width = 32 - 2;
            this.height = 48 - 2;
            this.bag = new Bag();
            this.moveable = new ActiveObject(this);
            create();
        }

        private void create()
        {
            body = GameModel.instance.bodyManager.createCharacterBody(this);
            TextureHelper helper = (TextureHelper)GameModel.instance.game.Services.GetService(typeof(TextureHelper));
            texture = helper.generateSimpleTexture(width, height, Color.BlanchedAlmond);

            GameModel.instance.keyboardInput.onPressedKeys += new KeyboardInput.onPressedKeysDelegate(onPressedKeysHandler);
        }

        private void onPressedKeysHandler(KeyboardInput.OnPressedKeysArgs args)
        {
            if (args.state.IsKeyDown(Keys.Left))
            {
                if (body.LinearVelocity.X > -50)
                {
                    body.LinearVelocity += new Vector2(-4, 0);
                }
            }
            if (args.state.IsKeyDown(Keys.Right))
            {
                if (body.LinearVelocity.X < 50)
                {
                    body.LinearVelocity += new Vector2(4, 0);
                }
            }
            if (args.state.IsKeyDown(Keys.Space))
            {
                if (body.LinearVelocity.Y > -2 && body.LinearVelocity.Y < 2)
                {
                    body.LinearVelocity += new Vector2(0, -75);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GameModel.instance.spriteBatch.Draw(texture, body.Position, null, Color.White, body.Rotation, new Vector2(width / 2f, height / 2f), 1f, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            if (body.LinearVelocity != Vector2.Zero)
            {
                moveable.UpdatePosition(body.Position);
            }
        }
    }
}
