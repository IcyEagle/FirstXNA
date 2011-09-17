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
        public delegate void MouseClickEventHandler (Object sender, MouseClickEventArgs args);
        public event MouseClickEventHandler mouseClick;

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;

        World world;

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

            world = new World(new Vector2(0, 100));

            GameModel.Init(this);

            // initialize services.
            Services.AddService(typeof(TextureHelper), new TextureHelper(this));
            Services.AddService(typeof(TerrainGenerator), new TerrainGenerator(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            TerrainGenerator helper = (TerrainGenerator)Services.GetService(typeof(TerrainGenerator));

            // initialize components.
            Block[,] map = helper.generateMap(SCREEN_WIDTH, SCREEN_HEIGHT, Terrain.BLOCK_SIZE, Terrain.BLOCK_SIZE);
            Components.Add(new Terrain(map, this));
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

            // TODO: Add your update logic here
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                MouseState state = Mouse.GetState();
                MouseClickEventArgs args = new MouseClickEventArgs(new Vector2(state.X, state.Y));
                mouseClick.Invoke(this, args);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
