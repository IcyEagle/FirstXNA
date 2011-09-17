using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XNA.model.block;

namespace XNA
{
    class GameModel
    {
        public static GameModel instance;

        public BlockManager blockManager;

        public static void Init(Game1 game)
        {
            instance = new GameModel(game);
        }

        private GameModel(Game1 game)
        {
            blockManager = new BlockManager(game);
            //...
        }

    }
}
