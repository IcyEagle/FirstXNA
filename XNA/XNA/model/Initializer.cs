using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA.model
{
    class Initializer
    {

        public static void init()
        {
            GameModel.instance.init();

            GameModel.instance.world = new World(new Vector2(0, 50));
            GameModel.instance.spriteBatch = new SpriteBatch(GameModel.instance.game.GraphicsDevice);

            // initialize services.
            GameModel.instance.game.Services.AddService(typeof(TextureHelper), new TextureHelper());
            GameModel.instance.game.Services.AddService(typeof(TerrainGenerator), new TerrainGenerator());
        }

    }
}
