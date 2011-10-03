using System;
using XNA.model;
using XNA.model.block;

namespace XNA
{
    class TerrainGenerator
    {
        public static int visibleBlockLayers = 2;

        public Block[,] generateMap(int width, int height)
        {
            
            var blockRowSize = (int)Math.Ceiling((double)width / Terrain.BLOCK_SIZE);
            var blockColumnSize = (int)Math.Ceiling((double)height / Terrain.BLOCK_SIZE);

            var blocks = new Block[blockRowSize, blockColumnSize];

            var rand = new Random();

            // feel only last 3 lines.
            for (int i = 0 ; i < blockRowSize ; ++i)
            {
                for (int j = 0; j < blockColumnSize; ++j)
                {
                    // make choice.
                    if ((j >= blockColumnSize - visibleBlockLayers && rand.Next() % 2 != 0) || ((i < visibleBlockLayers || i >= blockRowSize - visibleBlockLayers) && rand.Next() % 2 != 0))
                    {
                        // random texture.
                        blocks[i, j] = GameModel.Instance.GenericFactory.CreateBlock(((int)(rand.Next(1, 3)) == 1) ? "ground" : "stone", Terrain.BLOCK_SIZE * i, Terrain.BLOCK_SIZE * j);
                    }
                }
            }

            return blocks;
        }

    }
}
