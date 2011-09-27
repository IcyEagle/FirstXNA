using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace XNA.model.@base
{
    public class PhysicalObject : InteractiveObject
    {

        public Body Body;

        private float _restitution;
        private float _friction;

        public override void Update()
        {
            if (Body != null)
            {
                Position = Body.Position;
                Rotation = Body.Rotation;
            }
            base.Update();
        }

        public override Vector2 Position
        {
            set { base.Position = value; if (Body != null) Body.Position = value; }
        }

        public float Restitution
        {
            get { return _restitution; }
            set { _restitution = value; if (Body != null) Body.Restitution = _restitution; }
        }

        public float Friction
        {
            get { return _friction; }
            set { _friction = value; if (Body != null) Body.Friction = _friction; }
        }

                public void ThrowOut()
        {
            var rand = new Random();
            Body.LinearVelocity = new Vector2(rand.Next(-50, 50), rand.Next(-75, -50));
        }

        public void ApproachTo(Vector2 anchor, float force)
        {
            Vector2 diff = Body.Position - anchor;
            Body.LinearVelocity -= new Vector2(Math.Sign(diff.X) * force, Math.Sign(diff.Y) * force);
        }

    }
}
