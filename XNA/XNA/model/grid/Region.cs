using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA.model.grid
{
    /**
     * A particle of grid.
     */
    public class Region
    {

        public List<ActiveObject> members;

        public Region()
        {
            this.members = new List<ActiveObject>();
        }
    }
}
