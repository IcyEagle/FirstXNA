using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace XNA.model.@base
{
    public class PhysicalObject : InteractiveObject
    {

        public Body body;

        private float _restitution;
        private float _friction;

        public PhysicalObject()
        {
            
        }

        public override void Update()
        {
            if (body != null)
            {
                if (bodyNeedPosition)
                {
                    body.Position = new Vector2(x, y);
                    bodyNeedPosition = false;
                }
                else
                {
                    x = body.Position.X;
                    y = body.Position.Y;
                }
                rotation = body.Rotation;
            }
            base.Update();
        }

        public float restitution
        {
            get { return _restitution; }
            set { _restitution = value; if (body != null) body.Restitution = _restitution; }
        }

        public float friction
        {
            get { return _friction; }
            set { _friction = value; if (body != null) body.Friction = _friction; }
        }

    }
}
