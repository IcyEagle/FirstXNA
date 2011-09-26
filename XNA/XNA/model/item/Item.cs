using System;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using XNA.model.grid;

namespace XNA.model.item
{
    class Item : ActiveObject
    {
        public void ThrowOut()
        {
            var rand = new Random();
            Body.LinearVelocity = new Vector2(rand.Next(-50, 50), rand.Next(-75, -50));
        }

        public void ApproachTo(Vector2 anchor)
        {
            Vector2 diff = Body.Position - anchor;
            Body.LinearVelocity -= new Vector2(Math.Sign(diff.X) * 100, Math.Sign(diff.Y) * 100);
        }
    }
}
