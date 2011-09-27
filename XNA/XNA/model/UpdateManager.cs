using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA.model.@base;

namespace XNA.model
{
    class UpdateManager
    {

        private List<DrawableObject> drawableObjects;

        public UpdateManager()
        {
            drawableObjects = new List<DrawableObject>();
        }

        public void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) GameModel.Instance.Game.Exit();

            GameModel.Instance.MouseInput.Update();
            GameModel.Instance.KeyboardInput.Update();

            foreach (DrawableObject drawableObject in drawableObjects.ToArray())
            {
                drawableObject.Update();
            }

            GameModel.Instance.World.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            GameModel.Instance.Camera2D.Update();

        }

        public void addObjectForUpdate(DrawableObject drawableObject)
        {
            drawableObjects.Add(drawableObject);
        }

        public void removeObjectForUpdate(DrawableObject drawableObject)
        {
            drawableObjects.Remove(drawableObject);
        }

    }
}
