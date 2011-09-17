using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA.model;

namespace XNA
{
    class GameModel
    {
        public static GameModel instance;

        public BlockManager blockManager;
        public ItemManager itemManager;

        public static void Init(Game1 game)
        {
            instance = new GameModel(game);
        }

        private GameModel(Game1 game)
        {
            blockManager = new BlockManager(game);
            itemManager = new ItemManager(game);
            //...
        }

    }
}
