using System;
using Microsoft.Xna.Framework;
using XNA.model.@base;

namespace XNA.model.grid
{
    /**
     * Each object which should be tracked in grid should be derived from this class.
     */
    public class ActiveObject : PhysicalObject
    {
        private static int objectCounter = 0;

        // for Grid class as identifier.
        internal int objectID;

        // TEMP
        public Object master;

        public ActiveObject(/*Object master*/)
        {
            this.objectID = ++ActiveObject.objectCounter;
            master = this;
            //this.master = master;
        }

        public override void Update()
        {
            GameModel.instance.grid.moveTo(this, new Vector2(x, y));
            base.Update();
        }

        /*public void UpdatePosition(Vector2 coordinates)
        {
            GameModel.instance.grid.moveTo(this, coordinates);
        }*/
    }
}
