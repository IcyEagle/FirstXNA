using System;
using Microsoft.Xna.Framework;
using XNA.model.input;
using Microsoft.Xna.Framework.Input;
using XNA.model.grid;

namespace XNA.model.character
{
    public class Character : ActiveObject
    {

        public string name;
        public int level;

        private Bag bag;

        public Character(string name, int level)
        {
            this.name = name;
            this.level = level;
            this.bag = new Bag();
            width = 32 - 2;
            height = 48 - 2;
            create();
        }

        private void create()
        {
            body = GameModel.instance.bodyManager.createCharacterBody(this);
            var helper = (TextureHelper)GameModel.instance.game.Services.GetService(typeof(TextureHelper));
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

        /*public void Draw()
        {
            //GameModel.instance.spriteBatch.Draw(texture, new Vector2(x, y), null, Color.White, rotation, new Vector2(width / 2f, height / 2f), 1f, SpriteEffects.None, 0f);
            //GameModel.instance.spriteBatch.Draw(texture, body.Position, null, Color.White, body.Rotation, new Vector2(width / 2f, height / 2f), 1f, SpriteEffects.None, 0f);
            //GameModel.instance.spriteBatch.Draw(texture, new Rectangle((int)body.Position.X - 15, (int)body.Position.Y - 22, width, height), Color.White);
        }*/

        /*public void Update()
        {
            if (body.LinearVelocity != Vector2.Zero)
            {
                moveable.UpdatePosition(body.Position);
            }
        }*/
    }
}
