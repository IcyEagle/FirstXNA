using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNA.model
{
    class PhysicalActiveObject
    {
        private static int objectCounter = 0;

        private int id;

        public Vector2 position;

        public PhysicalActiveObject()
        {
            this.id = ++objectCounter;
        }

        public void updatePosition(Vector2 position)
        {
            if (this.position != Vector2.Zero)
            {
                GameModel.instance.terrain.movePhysicalObject(this, position);
            }
            else
            {
                GameModel.instance.terrain.placePhysicalObject(position);
            }

            // update current position.
            this.position = position;
        }
    }
}
