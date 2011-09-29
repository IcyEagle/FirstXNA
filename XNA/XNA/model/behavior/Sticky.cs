using System;
using Microsoft.Xna.Framework;
using XNA.model.@base;
using XNA.model.character;
using XNA.model.grid;

namespace XNA.model.behavior
{
    class Sticky : Behavior, IActive
    {
        private readonly PhysicalObject _master;
        private readonly float _distance;
        private readonly float _force;

        public Sticky(PhysicalObject master, float distance, float force)
        {
            _master = master;
            _distance = distance;
            _force = force;
        }

        public void OnActive(ActiveObject target, Vector2 coordinates)
        {
            Vector2 diff = coordinates - _master.Position;
            if (diff.Length() < _distance)
            {
                _master.ApproachTo(target.Position, _force);
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
