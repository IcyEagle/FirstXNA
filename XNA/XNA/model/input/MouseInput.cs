using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace XNA.model
{
    class MouseInput
    {

        public MouseInput()
        {

        }

        public delegate void onClickHandler(OnClickArgs args);
        public event onClickHandler onClick;
        public class OnClickArgs
        {
            public MouseState state;
            public OnClickArgs(MouseState state) { this.state = state; } 
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                onClick.Invoke(new OnClickArgs(Mouse.GetState()));
            }
        }

    }
}
