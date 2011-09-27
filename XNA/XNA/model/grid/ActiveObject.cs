using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNA.model.@base;
using XNA.model.behavior;

namespace XNA.model.grid
{
    /**
     * Each object which should be tracked in grid should be derived from this class.
     */
    public class ActiveObject : PhysicalObject
    {
        public delegate void OnMoveDelegate(ActiveObject target, Vector2 coordinates);
        public event OnMoveDelegate OnMove;

        private static int _objectCounter = 0;

        // for Grid class as identifier.
        internal int ObjectID;

        private ICollection<IBehavior> _behaviors = new List<IBehavior>();

        public ActiveObject()
        {
            ObjectID = ++_objectCounter;
        }

        public override void Update()
        {
            GameModel.Instance.Grid.moveTo(this, new Vector2(X, Y));
            base.Update();

            // fire event.
            if (OnMove != null) {OnMove.Invoke(this, Position);}
        }

        public void AddBehavior(IBehavior behavior)
        {
            _behaviors.Add(behavior);  
        }

        public void RemoveBehavior(IBehavior behavior)
        {
            _behaviors.Remove(behavior);
        }

        public ICollection<IBehavior> Behaviors { get { return _behaviors; } }
    }
}
