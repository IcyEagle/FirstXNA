using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA.model.character
{
    class BagItem
    {
        private Item item;

        public BagItem()
        {
        }

        public bool append(Item item)
        {
            if (this.item == null)
            {
                this.item = item;
                return true;
            }

            return false;
        }
    }
}
