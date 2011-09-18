﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA.model;
using XNA.model.input;

namespace XNA
{
    class GameModel
    {

        public static GameModel instance;

        public Character character;
        public ItemManager itemManager;
        public BodyManager bodyManager;

        public MouseInput mouseInput;
        public KeyboardInput keyboardInput;


        public static void Init(Game1 game)
        {
            instance = new GameModel(game);
        }

        private GameModel(Game1 game)
        {
            mouseInput = new MouseInput();
            keyboardInput = new KeyboardInput();
            
            bodyManager = new BodyManager(game.world);
            itemManager = new ItemManager(game);
            //...
        }

    }
}
