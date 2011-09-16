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
        public const int BLOCK_SIZE = 100;

        Block[,] map;

        public Terrain(Block[,] map, Game1 game) : base(game)
        {
            this.map = map;

            game.mouseClick += new Game1.MouseClickEventHandler(MouseClick);
        }

        public void MouseClick(Object target, MouseClickEventArgs args)
        {
            Game.Window.Title = args.position.X + " " + args.position.Y;

            int x = (int) args.position.X / BLOCK_SIZE;
            int y = (int)args.position.Y / BLOCK_SIZE;
            map[x, y] = null;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Block block in map)
            {
                if (block != null)
                {
                    block.Draw(((Game1)Game).spriteBatch);
                }
            }
        }
    }
}
