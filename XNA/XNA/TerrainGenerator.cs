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
using XNA.model.block;

namespace XNA
{
    class TerrainGenerator
    {
        private Game game;
        public static int visibleBlockLayers = 20;

        public TerrainGenerator(Game game)
        {
            this.game = game;
        }

        public Block[,] generateMap(int width, int height)
        {
            
            int blockRowSize = (int)Math.Ceiling((double)width / Terrain.BLOCK_SIZE);
            int blockColumnSize = (int)Math.Ceiling((double)height / Terrain.BLOCK_SIZE);

            Block[,] blocks = new Block[blockRowSize, blockColumnSize];

            //TextureHelper helper = (TextureHelper)game.Services.GetService(typeof(TextureHelper));

            // couple of available textures.
            /*Collection<Texture2D> textures = new Collection<Texture2D>();
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.DarkKhaki));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.Green));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.Silver));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.DarkGreen));
            textures.Add(helper.generateSimpleTexture(blockWidth, blockHeight, Color.DarkGray));*/

            Random rand = new Random();

            // feel only last 3 lines.
            for (int i = 0 ; i < blockRowSize ; ++i)
            {
                for (int j = 0; j < blockColumnSize; ++j)
                {
                    // make choice.
                    if ((j >= blockColumnSize - visibleBlockLayers && rand.Next() % 2 != 0) || ((i < visibleBlockLayers || i >= blockRowSize - visibleBlockLayers) && rand.Next() % 2 != 0))
                    {
                        // random texture.
                        //Texture2D texture = textures[rand.Next() % textures.Count];
                        blocks[i, j] = new Block(((int)(rand.Next(1, 3)) == 1) ? "ground" : "stone", Terrain.BLOCK_SIZE * i, Terrain.BLOCK_SIZE * j);
                    }
                }
            }

            return blocks;
        }

    }
}
