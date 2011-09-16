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

namespace XNA
{
    class TerrainGenerator
    {
        private Game game;

        List<Texture2D> texturesStack;

        public TerrainGenerator(Game game)
        {
            this.game = game;
        }

        public Block[,] generateMap(int width, int height, int blockWidth, int blockHeight)
        {
            Block[,] blocks = new Block[width, height];

            TextureHelper helper = (TextureHelper)game.Services.GetService(typeof(TextureHelper));

            // couple of available textures.
            Collection<Texture2D> textures = new Collection<Texture2D>();
            textures.Add(helper.generateSimpleTexture(width, height, Color.Brown));
            textures.Add(helper.generateSimpleTexture(width, height, Color.SteelBlue));
            textures.Add(helper.generateSimpleTexture(width, height, Color.Silver));
            textures.Add(helper.generateSimpleTexture(width, height, Color.Gold));
            textures.Add(helper.generateSimpleTexture(width, height, Color.LightGray));

            Random rand = new Random();

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    // random texture.
                    Texture2D texture = textures[rand.Next() % textures.Count];
                    blocks[i, j] = new Block(texture, new Rectangle(blockWidth * i, blockHeight * j, blockWidth, blockHeight));
                }
            }

            return blocks;
        }

    }
}
