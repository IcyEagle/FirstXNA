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

namespace XNA
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Block[,] blockMap;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            Capability.changeGraphicAdapter();
        }

        private Texture2D getDefaultTexture(Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, 30, 70);
            Color[] colorMap = new Color[30 * 70];
            for (int i = 0; i < colorMap.Length; i++) colorMap[i] = color;
            texture.SetData(colorMap);

            return texture;
        }

        protected override void Initialize()
        {
            int blockWidth = 40;
            int blockHeight = 40;

            Texture2D[] textures = new Texture2D[3];
            textures[0] = getDefaultTexture(Color.Yellow);
            textures[1] = getDefaultTexture(Color.Purple);
            textures[2] = getDefaultTexture(Color.SeaGreen);

            Random rand = new Random();
            blockMap = new Block[10, 10];
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    blockMap[i, j] = new Block(textures[rand.Next() % 3], new Rectangle(blockWidth * i, blockHeight * j, blockWidth, blockHeight));
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    blockMap[i, j].Draw(spriteBatch);
                }
            }

            base.Draw(gameTime);
        }
    }
}
