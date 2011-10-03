using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA.model;

namespace XNA
{
    public class Game1 : Game
    {

        readonly GraphicsDeviceManager _graphics;

        public const int ScreenWidth = 1024;
        public const int ScreenHeight = 768;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            
            IsMouseVisible = true;

            Capability.changeGraphicAdapter();
        }

        protected override void Initialize()
        {
            GameModel.Instance.Game = this;

            Initializer.init();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            GameModel.Instance.ContentManager.init();

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            GameModel.Instance.UpdateManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GameModel.Instance.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, GameModel.Instance.Camera2D.getTransformation());
            GameModel.Instance.DrawManager.Draw();
            base.Draw(gameTime);
            GameModel.Instance.SpriteBatch.End();
        }
    }
}
