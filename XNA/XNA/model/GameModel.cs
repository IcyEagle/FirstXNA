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

        public Character character;
        public ItemManager itemManager;
        public BodyManager bodyManager;

        public MouseInput mouseInput;


        public static void Init(Game1 game)
        {
            instance = new GameModel(game);
        }

        private GameModel(Game1 game)
        {
            bodyManager = new BodyManager(game.world);
            mouseInput = new MouseInput();
            itemManager = new ItemManager(game);
            //...
        }

    }
}
