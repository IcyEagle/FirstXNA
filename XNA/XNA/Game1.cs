using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA.model;

namespace XNA
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;

        public const int SCREEN_WIDTH = 1024;
        public const int SCREEN_HEIGHT = 768;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            
            IsMouseVisible = true;

            Capability.changeGraphicAdapter();
        }

        protected override void Initialize()
        {
            GameModel.instance.game = this;

            Initializer.init();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            GameModel.instance.contentManager.init();

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            GameModel.instance.updateManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GameModel.instance.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, GameModel.instance.camera2d.getTransformation());
            GameModel.instance.character.Draw();
            base.Draw(gameTime);
            GameModel.instance.spriteBatch.End();
        }
    }
}
