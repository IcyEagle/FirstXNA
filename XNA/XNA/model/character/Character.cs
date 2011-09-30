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
            //bag = new Bag();

            GameModel.Instance.KeyboardInput.onPressedKeys += new KeyboardInput.onPressedKeysDelegate(onPressedKeysHandler);
        }

        private void onPressedKeysHandler(KeyboardInput.OnPressedKeysArgs args)
        {
            if (args.state.IsKeyDown(Keys.Left))
            {
                if (Body.LinearVelocity.X > -10)
                {
                    Body.LinearVelocity += new Vector2(-1, 0);
                }
            }
            if (args.state.IsKeyDown(Keys.Right))
            {
                if (Body.LinearVelocity.X < 10)
                {
                    Body.LinearVelocity += new Vector2(1, 0);
                }
            }
            if (args.state.IsKeyDown(Keys.Space))
            {
                if (Body.LinearVelocity.Y > -2 && Body.LinearVelocity.Y < 2)
                {
                    Body.LinearVelocity += new Vector2(0, -15);
                }
            }
        }
    }
}
