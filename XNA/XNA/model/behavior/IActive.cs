using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNA.model.grid;

namespace XNA.model.behavior
{
    interface IActive
    {
        void OnActive(ActiveObject target, Vector2 coordinates);
    }
}
