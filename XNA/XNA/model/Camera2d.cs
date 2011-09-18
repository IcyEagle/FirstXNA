using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA.model
{
    class Camera2d
    {

        private Matrix transform;
        private Vector2 pos;
        private float rotation;
        private float zoom;

        MouseState oldMouse;

        public Camera2d()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public void Move(Vector2 amount)
        {
            pos += amount;
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Matrix getTransformation()
        {
            transform = Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                                                     Matrix.CreateRotationZ(Rotation) *
                                                     Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                                     Matrix.CreateTranslation(new Vector3(Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT, 0));
            return transform;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Move(new Vector2((Mouse.GetState().X - oldMouse.X) * -0.5f, (Mouse.GetState().Y - oldMouse.Y) * -0.5f));
                }

                if (Mouse.GetState().ScrollWheelValue != oldMouse.ScrollWheelValue)
                {
                    if (Zoom > 1)
                    {
                        Zoom += (Mouse.GetState().ScrollWheelValue - oldMouse.ScrollWheelValue) / 120.0f / 10.0f;
                    }
                    else
                    {
                        Zoom *= 1 + (Mouse.GetState().ScrollWheelValue - oldMouse.ScrollWheelValue) / 120.0f / 20.0f;
                    }
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    Rotation += ((Mouse.GetState().X - oldMouse.X) / 500.0f);
                    Rotation += ((Mouse.GetState().Y - oldMouse.Y) / 500.0f);
                }

                oldMouse = Mouse.GetState();
            }
            else
            {
                Pos = new Vector2(GameModel.instance.character.body.Position.X + Game1.SCREEN_WIDTH / 2, GameModel.instance.character.body.Position.Y + Game1.SCREEN_HEIGHT / 2);
            }
        }

    }
}
