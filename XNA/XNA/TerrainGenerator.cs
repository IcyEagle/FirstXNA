using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;
using System.Collections.ObjectModel;

using XNA.model;

namespace XNA
{
    class TerrainGenerator
    {
        private Game game;

        public TerrainGenerator(Game game)
        {
            this.game = game;
        }

        public Block[,] generateMap(int width, int height, int blockWidth, int blockHeight)
        {
            
            int blockRowSize = (int)Math.Ceiling((double)width / blockWidth);
            int blockColumnSize = (int)Math.Ceiling((double)height / blockHeight);

            Block[,] blocks = new Block[blockRowSize, blockColumnSize];
            game.Window.Title = blocks.Length.ToString();

            TextureHelper helper = (TextureHelper)game.Services.GetService(typeof(TextureHelper));

            // couple of available textures.
            Collection<Texture2D> textures = new Collection<Texture2D>();
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.Brown));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.SteelBlue));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.Silver));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.Gold));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.LightGray));

            Random rand = new Random();

            // feel only last 3 lines.
            for (int i = 0 ; i < blockRowSize ; ++i)
            {
                for (int j = blockColumnSize - 3; j < blockColumnSize; ++j)
                {
                    // make choice.
                    if (rand.Next() % 2 != 0)
                    {
                        // random texture.
                        Texture2D texture = textures[rand.Next() % textures.Count];
                        blocks[i, j] = new Block(texture, new Rectangle(blockWidth * i, blockHeight * j, blockWidth, blockHeight));
                    }
                }
            }

            return blocks;
        }

    }
}
