using System;
using Microsoft.Xna.Framework;
using XNA.model.@base;
using XNA.model.character;
using XNA.model.grid;
using XNA.model.item;

namespace XNA.model.behavior
{
    class Pickup : Behavior, IActive
    {
        private readonly ActiveObject _master;
        private readonly float _distance;

        public Pickup(ActiveObject master, float distance)
        {
            _master = master;
            _distance = distance;
        }

        public void OnActive(ActiveObject target, Vector2 coordinates)
        {
            Vector2 diff = coordinates - _master.Position;
            if (diff.Length() < _distance)
            {
                // TODO pick up item.
                _master.Destroy();
            }
        }

        public override void Disable()
        {
            GameModel.Instance.Character.OnMove -= OnActive;
            base.Disable();
        }

        public override void Enable(ActiveObject caller)
        {
            if (caller is Character)
            {
                GameModel.Instance.Character.OnMove += OnActive;
            }

            base.Enable(caller);
        }
    }
}
