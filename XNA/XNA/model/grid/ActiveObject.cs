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

        public ActiveObject()
        {
            objectID = ++objectCounter;
        }

        public override void Update()
        {
            GameModel.instance.grid.moveTo(this, new Vector2(_x, _y));
            base.Update();
        }
    }
}
