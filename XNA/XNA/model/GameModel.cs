using XNA.model.character;
using XNA.model.input;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using XNA.model.grid;
using XNA.model.physical;
using XNA.model.listener;

namespace XNA.model
{
    class GameModel
    {
        public static GameModel Instance = new GameModel();

        public Game1 Game;
        public World World;
        public SpriteBatch SpriteBatch;
        public Character Character;
        public Terrain Terrain;
        public ContentManager ContentManager = new ContentManager();
        public UpdateManager UpdateManager = new UpdateManager();
        public Camera2d Camera2D = new Camera2d();
        public ItemManager ItemManager = new ItemManager();
        public BodyManager BodyManager = new BodyManager();
        public Grid Grid = new Grid();
        public GenericFactory GenericFactory = new GenericFactory();
        public TextureHelper TextureHelper = new TextureHelper();

        public MouseInput MouseInput = new MouseInput();
        public KeyboardInput KeyboardInput = new KeyboardInput();

        public PhysicalManager PhysicalManager;

        public CharacterListener CharacterListener;
        public ItemListener ItemListener;

        public void Init()
        {
            PhysicalManager = new PhysicalManager();
            CharacterListener = new CharacterListener();
            ItemListener = new ItemListener();
        }
    }
}
