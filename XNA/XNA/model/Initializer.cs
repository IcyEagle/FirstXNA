using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA.model
{
    class Initializer
    {

        public static void init()
        {
            GameModel.Instance.Init();

            GameModel.Instance.World = new World(new Vector2(0, 50));
            GameModel.Instance.SpriteBatch = new SpriteBatch(GameModel.Instance.Game.GraphicsDevice);

            // initialize services.
            GameModel.Instance.Game.Services.AddService(typeof(TerrainGenerator), new TerrainGenerator());
        }

    }
}
