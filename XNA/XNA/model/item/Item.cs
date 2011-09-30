using XNA.model.behavior;
using XNA.model.grid;

namespace XNA.model.item
{
    class Item : ActiveObject
    {
        public override void Activate(ActiveObject caller)
        {
            foreach (Behavior behavior in _behaviors)
            {
                if (!behavior.Enabled)
                {
                    behavior.Enable(caller);
                }
            }
        }

        public override void Deactivate(ActiveObject caller)
        {
            foreach (Behavior behavior in _behaviors)
            {
                if (behavior.Enabled)
                {
                    behavior.Disable();
                }
            }
        }
    }
}
