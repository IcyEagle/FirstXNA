﻿using System;
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

using XNA.model;

namespace XNA
{
    class Terrain : DrawableGameComponent
    {
        public const int BLOCK_SIZE = 100;

        Block[,] map;

        public Terrain(Block[,] map, Game1 game) : base(game)
        {
            this.map = map;

            GameModel.instance.mouseInput.onClick += new MouseInput.onClickHandler(MouseClick);

            buildPhysicsModel();
            init();
        }

        public void MouseClick(MouseInput.OnClickArgs args)
        {
            // calculate block position.
            int x = (int) args.state.X / BLOCK_SIZE;
            int y = (int) args.state.Y / BLOCK_SIZE;

            Game.Window.Title = x + ", " + y;

            Block block = map[x, y];

            // damage block.
            if (map[x, y] != null)
            {
                bool destroyed = block.damage();
                if (destroyed)
                {
                    map[x, y] = null;
                    updatePhysicsAround(x, y);
                }
            }
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

        protected void buildPhysicsModel()
        {
            for (int x = 0 ; x < map.GetUpperBound(0) ; ++x)
            {
                for (int y = 0; y < map.GetUpperBound(1); ++y)
                {
                    Block block = map[x, y];
                    if (block != null && (!hasLeftNeighbor(x, y) || !hasRightNeighbor(x, y) || !hasUpNeighbor(x, y) || !hasDownNeighbor(x, y)))
                    {
                        block.enablePhysics();
                    }
                }
            }
        }

        protected void init()
        {
            foreach (Block block in map)
            {
                if (block != null)
                {
                    block.onDestroy += new Block.onDestroyHandler(onBlockDestroyHandler);
                }
            }
        }

        protected void updatePhysicsAround(int x, int y)
        {
            // erase cell.
            map[x, y] = null;

            // enable neighbors' physics.
            // Where:
            //   # - enabled physics,
            //   % - disabled physics,
            //   X - deleted element
            // %%%%%%#  #%%%    %%%%%%#  #%%%     %%%%%%#  #%%%
            // %%%%%%#  #%%% -> %%%%%%X  #%%% ->  %%%%%#   #%%%
            // %%%%%%%##%%%%    %%%%%%%##%%%%     %%%%%%###%%%%

            if (hasLeftNeighbor(x, y)) { getLeftNeighbor(x, y).enablePhysics(); }
            if (hasRightNeighbor(x, y)) { getRightNeighbor(x, y).enablePhysics(); }
            if (hasUpNeighbor(x, y)) { getUpNeighbor(x, y).enablePhysics(); }
            if (hasDownNeighbor(x, y)) { getDownNeighbor(x, y).enablePhysics(); }
        }

        protected void onBlockDestroyHandler(Block block)
        {
            Item item = GameModel.instance.itemManager.getItem(ItemManager.ItemType.BLOCK_GENERIC);
        }

        private bool hasLeftNeighbor(int x, int y) {
            return x > 0 && map[x - 1, y] != null;
        }

        private Block getLeftNeighbor(int x, int y) {
            return map[x - 1, y];
        }

        private bool hasRightNeighbor(int x, int y) {
            return x < map.GetUpperBound(0) && map[x + 1, y] != null;
        }

        private Block getRightNeighbor(int x, int y) {
            return map[x + 1, y];
        }

        private bool hasUpNeighbor(int x, int y) {
            return y > 0 && map[x, y - 1] != null;
        }

        private Block getUpNeighbor(int x, int y) {
            return map[x, y - 1];
        }

        private bool hasDownNeighbor(int x, int y) {
            return y < map.GetUpperBound(1) && map[x, y + 1] != null;
        }

        private Block getDownNeighbor(int x, int y) {
            return map[x, y + 1];
        }

    }
}
