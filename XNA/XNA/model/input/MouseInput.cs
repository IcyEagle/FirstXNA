using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace XNA.model.input
{
    class MouseInput
    {

        public delegate void OnClickDelegate(OnClickArgs args);
        public event OnClickDelegate OnClick;
        public class OnClickArgs
        {
            public MouseState State;
            public OnClickArgs(MouseState state) { State = state; } 
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && OnClick != null)
            {
                OnClick.Invoke(new OnClickArgs(Mouse.GetState()));
            }
        }

        public static Vector2 ToAbsolute(Vector2 relative)
        {
            Vector2 absolute;
            Matrix inverse;
            Matrix matrix = GameModel.Instance.Camera2D.getTransformation();
            Matrix.Invert(ref matrix, out inverse);
            var pos = new Vector2(relative.X, relative.Y);
            Vector2.Transform(ref pos, ref inverse, out absolute);
            return absolute;
        }
    }
}
