using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace XNA.model
{
    class MouseInput
    {

        public MouseInput()
        {

        }

        public delegate void onClickDelegate(OnClickArgs args);
        public event onClickDelegate onClick;
        public class OnClickArgs
        {
            public MouseState state;
            public OnClickArgs(MouseState state) { this.state = state; } 
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && onClick != null)
            {
                onClick.Invoke(new OnClickArgs(Mouse.GetState()));
            }
        }

        public static Vector2 toAbsolute(Vector2 relative)
        {
            Vector2 absolute;
            Matrix inverse;
            Matrix matrix = GameModel.Instance.Camera2D.getTransformation();
            Matrix.Invert(ref matrix, out inverse);
            Vector2 pos = new Vector2(relative.X, relative.Y);
            Vector2.Transform(ref pos, ref inverse, out absolute);
            return absolute;
        }
    }
}
