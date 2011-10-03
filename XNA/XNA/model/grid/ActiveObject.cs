using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XNA.model.@base;
using XNA.model.behavior;
using XNA.model.item;

namespace XNA.model.grid
{
    /**
     * Each object which should be tracked in grid should be derived from this class.
     */
    abstract public class ActiveObject : PhysicalObject
    {
        public delegate void OnMoveDelegate(ActiveObject target, Vector2 coordinates);
        public event OnMoveDelegate OnMove;

        private static int _objectCounter;

        // for Grid class as identifier.
        internal int ObjectID;

        protected ICollection<Behavior> Behaviors = new List<Behavior>();

        protected ActiveObject()
        {
            ObjectID = ++_objectCounter;
        }

        public override void Update()
        {
            GameModel.Instance.Grid.MoveTo(this, new Vector2(X, Y));
            base.Update();

            // fire event.
            if (OnMove != null) {OnMove.Invoke(this, Position);}
        }

        public void AddBehavior(Behavior behavior)
        {
            Behaviors.Add(behavior);
        }

        public void RemoveBehavior(Behavior behavior)
        {
            Behaviors.Remove(behavior);
        }

        public abstract void Activate(ActiveObject caller);

        public abstract void Deactivate(ActiveObject caller);

        public void Destroy()
        {
            Deactivate(this);
            GameModel.Instance.UpdateManager.RemoveObjectForUpdate(this);
            GameModel.Instance.DrawManager.RemoveObjectForDraw(this);
            GameModel.Instance.Terrain.items.Remove(this as Item);
            GameModel.Instance.Grid.Remove(this);
        }
    }
}
