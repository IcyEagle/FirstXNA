using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNA.model.@base;

namespace XNA.model
{
    class UpdateManager
    {

        private readonly List<DrawableObject> _drawableObjects;

        public UpdateManager()
        {
            _drawableObjects = new List<DrawableObject>();
        }

        public void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) GameModel.Instance.Game.Exit();

            GameModel.Instance.MouseInput.Update();
            GameModel.Instance.KeyboardInput.Update();

            foreach (DrawableObject drawableObject in _drawableObjects.ToArray())
            {
                drawableObject.Update();
            }

            GameModel.Instance.World.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * .001f);

            GameModel.Instance.Camera2D.Update();

        }

        public void AddObjectForUpdate(DrawableObject drawableObject)
        {
            _drawableObjects.Add(drawableObject);
        }

        public void RemoveObjectForUpdate(DrawableObject drawableObject)
        {
            _drawableObjects.Remove(drawableObject);
        }

    }
}
