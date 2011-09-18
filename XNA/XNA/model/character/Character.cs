using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNA.model.input;
using Microsoft.Xna.Framework.Input;

namespace XNA.model
{
    class Character
    {

        private Game1 game;

        public string name;
        public int level;

        public int x;
        public int y;
        public int width;
        public int height;

        public Body body;
        public Texture2D texture;

        public Character(Game1 game, string name, int level)
        {
            this.game = game;
            this.name = name;
            this.level = level;
            width = 32;
            height = 48;
            create();
        }

        private void create()
        {
            body = GameModel.instance.bodyManager.createCharacterBody(this);
            TextureHelper helper = (TextureHelper)game.Services.GetService(typeof(TextureHelper));
            texture = helper.generateSimpleTexture(width, height, Color.BlanchedAlmond);

            GameModel.instance.keyboardInput.onPressedKeys += new KeyboardInput.onPressedKeysHandler(onPressedKeysHandler);
        }

        private void onPressedKeysHandler(KeyboardInput.OnPressedKeysArgs args)
        {
            if (args.state.IsKeyDown(Keys.Left))
            {
                if (body.LinearVelocity.X > -50)
                {
                    body.LinearVelocity = new Vector2(-50, body.LinearVelocity.Y);
                }
            }
            if (args.state.IsKeyDown(Keys.Right))
            {
                if (body.LinearVelocity.X < 50)
                {
                    body.LinearVelocity = new Vector2(50, body.LinearVelocity.Y);
                }
            }
            if (args.state.IsKeyDown(Keys.Space))
            {
                if (body.LinearVelocity.Y > -2 && body.LinearVelocity.Y < 2)
                {
                    body.LinearVelocity = new Vector2(body.LinearVelocity.X, -75);
                }
            }
        }

        public void Draw()
        {
            game.spriteBatch.Draw(texture, body.Position, null, Color.White, body.Rotation, new Vector2(width / 2f, height / 2f), 1f, SpriteEffects.None, 0f);
        }

    }
}
