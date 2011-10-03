using System;
using XNA.model.behavior;
using XNA.model.character;
using XNA.model.grid;

namespace XNA.model.item
{
    class Item : ActiveObject
    {

        public override void Activate(ActiveObject caller)
        {
            if (caller == this || caller is Character)
            {
                foreach (Behavior behavior in Behaviors)
                {
                    if (!behavior.Enabled)
                    {
                        behavior.Enable(caller);
                    }
                }
            }
        }

        public override void Deactivate(ActiveObject caller)
        {
            if (caller == this || caller is Character)
            {
                foreach (Behavior behavior in Behaviors)
                {
                    if (behavior.Enabled)
                    {
                        behavior.Disable();
                    }
                }
            }
        }
    }
}
