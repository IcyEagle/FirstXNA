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

namespace XNA.model
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
        private int[,] regionMap;

        // TEMP.
        List<Item> items = new List<Item>();

        public Terrain(Block[,] map, Game1 game) : base(game)
        {
            this.map = map;

            // init region map.
            regionMap = new int[(int)Math.Ceiling((float)map.GetUpperBound(0) / BLOCK_PER_PHYSICAL_REGION), (int)Math.Ceiling((float)map.GetUpperBound(1) / BLOCK_PER_PHYSICAL_REGION)];

            GameModel.instance.mouseInput.onClick += new MouseInput.onClickDelegate(onClickHandler);

            //buildPhysicsModel();
            init();
        }

        public void onClickHandler(MouseInput.OnClickArgs args)
        {
            Vector2 blockPosition = calculateBlockPositionByCoordinate(args.state.X, args.state.Y);
            Block block = map[(int)blockPosition.X, (int)blockPosition.Y];
            if (x > map.GetUpperBound(0) || y > map.GetUpperBound(1) || x < map.GetLowerBound(0) || y < map.GetLowerBound(1))
            {
                return;
            }


            // damage block.
            if (block != null)
            {
                bool destroyed = block.damage();
                if (destroyed)
                {
                    map[(int)blockPosition.X, (int)blockPosition.Y] = null;
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
            // current and all around regions reacted.
            int fromX = (int)position.X - 1 >= 0 ? (int)position.X - 1 : 0;
            int toX = (int)position.X + 1 < regionMap.GetUpperBound(0) ? (int)position.X + 1 : regionMap.GetUpperBound(0);
            int fromY = (int)position.Y - 1 >= 0 ? (int)position.Y - 1 : 0;
            int toY = (int)position.Y + 1 < regionMap.GetUpperBound(1) ? (int)position.Y + 1 : regionMap.GetUpperBound(1);

            for (int x = fromX; x <= toX; ++x)
            {
                for (int y = fromY; y <= toY; ++y)
                {
                    if (regionMap[x, y] == 0 && state == PhysicalRegionState.ENTER)
                    {
                        changeBlocksInRegion(new Vector2(x, y), state);
                    }

                    regionMap[x, y] += (int)state;

                    if (regionMap[x, y] == 0)
                    {
                        changeBlocksInRegion(new Vector2(x, y), state);
                    }

                    // ASSERT.
                    if (regionMap[x, y] < 0)
                    {
                        throw new Exception("Bad Physical map value: " + regionMap[x, y]);
                    }
                }
            }
        }

        private void changeBlocksInRegion(Vector2 position, PhysicalRegionState state)
        {
            int fromX = (int)position.X * BLOCK_PER_PHYSICAL_REGION;
            int toX = ((int)position.X + 1) * BLOCK_PER_PHYSICAL_REGION < map.GetUpperBound(0) ? ((int)position.X + 1) * BLOCK_PER_PHYSICAL_REGION : map.GetUpperBound(0) + 1;
            int fromY = (int)position.Y * BLOCK_PER_PHYSICAL_REGION;
            int toY = ((int)position.Y + 1) * BLOCK_PER_PHYSICAL_REGION < map.GetUpperBound(1) ? ((int)position.Y + 1) * BLOCK_PER_PHYSICAL_REGION : map.GetUpperBound(1) + 1;

            //Console.WriteLine("Update blocks from " + fromX + " to " + toX + " and from " + fromY + " to " + toY);

            for (int x = fromX; x < toX; ++x)
            {
                for (int y = fromY; y < toY; ++y)
                {
                    if (map[x, y] != null)
                    {
                        if (state == PhysicalRegionState.ENTER)
                        {
                            map[x, y].enablePhysics();
                        }

                        if (state == PhysicalRegionState.LEAVE)
                        {
                            map[x, y].disablePhysics();
                        }
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
                    block.Draw(GameModel.instance.spriteBatch);
                }
            }

            foreach (Item item in items)
            {
                item.Draw(GameModel.instance.spriteBatch);
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

        public override void Update(GameTime gameTime)
        {
            foreach (Item item in items)
            {
                item.Update();
            }
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
