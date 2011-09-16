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

namespace XNA
{
    class Terrain : DrawableGameComponent
    {
        public const int BLOCK_SIZE = 40;

        Block[,] map;

        public Terrain(Block[,] map, Game1 game) : base(game)
        {
            this.map = map;

            game.mouseClicked += new EventHandler(MouseClicked);
        }

        public void MouseClicked(Object target, EventArgs args)
        {
            Game.Window.Title = "Clicked";
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Block block in map)
            {
                block.Draw(((Game1)Game).spriteBatch);
            }
        }
    }
}
