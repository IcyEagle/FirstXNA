using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

using XNA.model;

namespace XNA
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public const int SCREEN_WIDTH = 1024;
        public const int SCREEN_HEIGHT = 768;

        public World world;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameModel.Init(this);

            world = new World(new Vector2(0, 50));

            GameModel.Init(this);

            // initialize services.
            Services.AddService(typeof(TextureHelper), new TextureHelper(this));
            Services.AddService(typeof(TerrainGenerator), new TerrainGenerator(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            TerrainGenerator helper = (TerrainGenerator)Services.GetService(typeof(TerrainGenerator));


            //DEBUG
            TextureHelper textureHelper = (TextureHelper)Services.GetService(typeof(TextureHelper));
            Block.enabledTexture = textureHelper.generateSimpleTexture(Terrain.BLOCK_SIZE, Terrain.BLOCK_SIZE, Color.White);

            // initialize components.
            GameModel.instance.character = new Character(this, "Griff", 1);
            Components.Add(GameModel.instance.character);

            Block[,] map = helper.generateMap(SCREEN_WIDTH, SCREEN_HEIGHT, Terrain.BLOCK_SIZE, Terrain.BLOCK_SIZE);
            GameModel.instance.terrain = new Terrain(map, this);
            Components.Add(GameModel.instance.terrain);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            GameModel.instance.mouseInput.Update();
            GameModel.instance.keyboardInput.Update();

            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            GameModel.instance.camera2d.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, GameModel.instance.camera2d.getTransformation());
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
