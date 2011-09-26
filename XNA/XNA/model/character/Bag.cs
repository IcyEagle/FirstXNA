using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNA.model.item;

namespace XNA.model.character
{
    class Bag
    {
        const int BAG_WIDTH = 10;
        const int BAG_HEIGHT = 5;

        private Dictionary<Vector2, BagItem> items;

        public Bag()
        {
            this.items = new Dictionary<Vector2, BagItem>();
        }

        public bool push(Item item) {
            Vector2? cell = findFreeSpace();

            if (cell != null)
            {
                BagItem bagItem = new BagItem();
                bagItem.append(item);
                items.Add((Vector2)cell, bagItem);
                return true;
            }

            return false;
        }

        private Vector2? findFreeSpace()
        {
            for (int i = 0; i < BAG_WIDTH; ++i)
            {
                for (int j = 0; j < BAG_HEIGHT; ++j)
                {
                    Vector2 position = new Vector2(i, j);
                    if (items.ContainsKey(position))
                    {
                        return position;
                    }
                }
            }

            return null;
        }
    }
}
