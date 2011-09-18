using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace XNA.model.input
{
    class KeyboardInput
    {

        public delegate void onPressedKeysDelegate(OnPressedKeysArgs args);
        public event onPressedKeysDelegate onPressedKeys;
        public class OnPressedKeysArgs
        {
            public KeyboardState state;
            public OnPressedKeysArgs(KeyboardState state) { this.state = state; }
        }

        private KeyboardState oldState;

        public void Update()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.GetPressedKeys().Length > 0 && onPressedKeys != null)
            {
                onPressedKeys.Invoke(new OnPressedKeysArgs(Keyboard.GetState()));
            }
            oldState = newState;
        }
    }
}
