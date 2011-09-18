using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA.model;
using XNA.model.input;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace XNA
{
    class GameModel
    {
        public static GameModel instance = new GameModel();

        public Game1 game;
        public World world;
        public SpriteBatch spriteBatch;
        public Character character;
        public Terrain terrain;
        public Camera2d camera2d = new Camera2d();
        public ItemManager itemManager = new ItemManager();
        public BodyManager bodyManager = new BodyManager();

        public MouseInput mouseInput = new MouseInput();
        public KeyboardInput keyboardInput = new KeyboardInput();
    }
}
