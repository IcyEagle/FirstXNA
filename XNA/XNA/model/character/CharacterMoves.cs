using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA.model.input;

namespace XNA.model.character
{
    class CharacterMoves
    {

        private readonly Character _character;

        public CharacterMoves(Character character)
        {
            _character = character;
            GameModel.Instance.KeyboardInput.onPressedKeys += onPressedKeysHandler;
        }

        private void onPressedKeysHandler(KeyboardInput.OnPressedKeysArgs args)
        {
            if (args.state.IsKeyDown(Keys.Left))
            {
                if (_character.Body.LinearVelocity.X > -10)
                {
                    _character.Body.LinearVelocity += new Vector2(-1, 0);
                }
            }
            if (args.state.IsKeyDown(Keys.Right))
            {
                if (_character.Body.LinearVelocity.X < 10)
                {
                    _character.Body.LinearVelocity += new Vector2(1, 0);
                }
            }
            if (args.state.IsKeyDown(Keys.Space))
            {
                if (_character.Body.LinearVelocity.Y > -2 && _character.Body.LinearVelocity.Y < 2)
                {
                    _character.Body.LinearVelocity += new Vector2(0, -15);
                }
            }
        }
    }
}
