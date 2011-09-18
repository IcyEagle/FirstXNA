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

using XNA.model;

namespace XNA
{
    class Terrain : DrawableGameComponent
    {
        public static int BLOCK_SIZE = 16;

        const int BLOCK_PER_PHYSICAL_REGION = 4;

        private enum PhysicalRegionState {
            LEAVE = -1,
            ENTER = 1
        };

        private Block[,] map;
        private int[,] physicalMap;

        // TEMP.
        List<Item> items = new List<Item>();

        public Terrain(Block[,] map, Game1 game) : base(game)
        {
            this.map = map;

            // init physical map.
            physicalMap = new int[(int)Math.Ceiling((float)map.GetLowerBound(0) / BLOCK_PER_PHYSICAL_REGION), (int)Math.Ceiling((float)map.GetLowerBound(1) / BLOCK_PER_PHYSICAL_REGION)];

            GameModel.instance.mouseInput.onClick += new MouseInput.onClickHandler(onClickHandler);

            //buildPhysicsModel();
            init();
        }

        public void onClickHandler(MouseInput.OnClickArgs args)
        {
            Vector2 blockPosition = calculateBlockPositionByCoordinate(args.state.X, args.state.Y);
            Block block = map[(int)blockPosition.X, (int)blockPosition.Y];

            // damage block.
            if (block != null)
            {
                bool destroyed = block.damage();
                if (destroyed)
                {
                    map[(int)blockPosition.X, (int)blockPosition.Y] = null;

                    // DEPRECATED!
                    updatePhysicsAround((int)blockPosition.X, (int)blockPosition.Y);
                }
            }
        }

        private Vector2 calculateBlockPositionByCoordinate(int x, int y)
        {
            return new Vector2(x / BLOCK_SIZE, y / BLOCK_SIZE);
        }

        private Vector2 calculateRegionByCoordinate(int x, int y)
        {
            return new Vector2(x / (BLOCK_SIZE * BLOCK_PER_PHYSICAL_REGION), y / (BLOCK_SIZE * BLOCK_PER_PHYSICAL_REGION));
        }

        public void movePhysicalObject(PhysicalActiveObject activeObject, Vector2 to)
        {
            Vector2 from = activeObject.position;
            Vector2 regionFrom = calculateRegionByCoordinate((int)from.X, (int)from.Y);
            Vector2 regionTo = calculateRegionByCoordinate((int)to.X, (int)to.Y);

            if (regionFrom != regionTo)
            {
                leavePhysicalRegion(regionFrom);
                enterPhysicalRegion(regionTo);
            }
        }

        public void placePhysicalObject(Vector2 to)
        {
            Vector2 regionTo = calculateRegionByCoordinate((int)to.X, (int)to.Y);
            updatePhysicalRegion(regionTo, PhysicalRegionState.ENTER);
        }

        private void leavePhysicalRegion(Vector2 position)
        {
            updatePhysicalRegion(position, PhysicalRegionState.LEAVE);
        }

        private void enterPhysicalRegion(Vector2 position)
        {
            updatePhysicalRegion(position, PhysicalRegionState.ENTER);
        }

        private void updatePhysicalRegion(Vector2 position, PhysicalRegionState state)
        {
            int fromX = (int) position.X * BLOCK_PER_PHYSICAL_REGION;
            int toX = (int)position.X * (BLOCK_PER_PHYSICAL_REGION + 1);
            int fromY = (int)position.Y * BLOCK_PER_PHYSICAL_REGION;
            int toY = (int)position.Y * (BLOCK_PER_PHYSICAL_REGION + 1);

            Console.WriteLine("Update region from " + fromX + " to " + toX + " and from " + fromY + " to " + toY);

            for (int x = fromX; x < toX; ++x)
            {
                for (int y = fromY; y < toY; ++y)
                {
                    if (physicalMap[x, y] == 0 && state > 0)
                    {
                        map[x, y].enablePhysics();
                    }

                    physicalMap[x, y] += (int)state;

                    if (physicalMap[x, y] == 0)
                    {
                        map[x, y].disablePhysics();
                    }

                    // ASSERT.
                    if (physicalMap[x, y] < 0)
                    {
                        throw new Exception("Bad Physical map value: " + physicalMap[x, y]);
                    }
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

            foreach (Item item in items)
            {
                item.Draw(((Game1)Game).spriteBatch);
            }
        }

        protected void buildPhysicsModel()
        {
            for (int x = 0 ; x <= map.GetUpperBound(0) ; ++x)
            {
                for (int y = 0; y <= map.GetUpperBound(1) ; ++y)
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

        // DEPRECATED!
        protected void updatePhysicsAround(int x, int y)
        {
            // erase cell.
            map[x, y] = null;

            // enable neighbors' physics.
            // Where:
            //   # - enabled physics,
            //   % - disabled physics,
            //   X - deleted element
            // %%%%%%#  #%%%    %%%%%%#  #%%%    %%%%%%#  #%%%
            // %%%%%%#  #%%% -> %%%%%%X  #%%% -> %%%%%#   #%%%
            // %%%%%%%##%%%%    %%%%%%%##%%%%    %%%%%%###%%%%

            if (hasLeftNeighbor(x, y)) { getLeftNeighbor(x, y).enablePhysics(); }
            if (hasRightNeighbor(x, y)) { getRightNeighbor(x, y).enablePhysics(); }
            if (hasUpNeighbor(x, y)) { getUpNeighbor(x, y).enablePhysics(); }
            if (hasDownNeighbor(x, y)) { getDownNeighbor(x, y).enablePhysics(); }
        }

        protected void onBlockDestroyHandler(Block block)
        {
            block.disablePhysics();
            Item item = GameModel.instance.itemManager.getItem(ItemManager.ItemType.BLOCK_GENERIC);
            Random rand = new Random();
            float offsetX = rand.Next(30, 170) / 100f;
            float offsetY = rand.Next(30, 170) / 100f;
            item.setPosition(block.x + block.width * offsetX / 2f, block.y + block.height * offsetY / 2f);
            item.throwOut();
            items.Add(item);
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
