using Microsoft.Xna.Framework;
using XNA.model.@base;

namespace XNA.model.grid
{
    /**
     * Each object which should be tracked in grid should be derived from this class.
     */
    public class ActiveObject : PhysicalObject
    {
        // TODO Should be replaced into base class.
        public delegate void onMoveDelegate(ActiveObject target, Vector2 coordinates);
        public event onMoveDelegate onMove;

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

            if (onMove != null)
            {
                onMove.Invoke(this, Position);
            }
        }
    }
}
