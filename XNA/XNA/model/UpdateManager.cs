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

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) GameModel.instance.game.Exit();

            GameModel.instance.mouseInput.Update();
            GameModel.instance.keyboardInput.Update();

            foreach (DrawableObject drawableObject in drawableObjects.ToArray())
            {
                drawableObject.Update();
            }

            GameModel.instance.world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            GameModel.instance.camera2d.Update();

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
