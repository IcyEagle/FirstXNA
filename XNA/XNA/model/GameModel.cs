using XNA.model;
using XNA.model.character;
using XNA.model.input;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using XNA.model.grid;
using XNA.model.physical;
using XNA.model.listener;

namespace XNA
{
    class GameModel
    {
        public static GameModel instance = new GameModel();

        public Game1 game;
        public World world;
        public SpriteBatch spriteBatch;
        public Character character;
        public Terrain terrain;
        public ContentManager contentManager = new ContentManager();
        public UpdateManager updateManager = new UpdateManager();
        public Camera2d camera2d = new Camera2d();
        public ItemManager itemManager = new ItemManager();
        public BodyManager bodyManager = new BodyManager();
        public Grid grid = new Grid();
        public GenericFactory genericFactory = new GenericFactory();

        public MouseInput mouseInput = new MouseInput();
        public KeyboardInput keyboardInput = new KeyboardInput();

        public PhysicalManager physicalManager;

        public CharacterListener characterListener;

        public void init()
        {
            physicalManager = new PhysicalManager();
            characterListener = new CharacterListener();
        }
    }
}
