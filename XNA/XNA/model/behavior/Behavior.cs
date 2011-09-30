using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA.model.grid;

namespace XNA.model.behavior
{
    abstract public class Behavior
    {
        private bool _enabled = false;

        public bool Enabled
        {
            get { return _enabled; }
            protected set { _enabled = value; }
        }

        public virtual void Disable()
        {
            _enabled = false;
        }

        public virtual void Enable(ActiveObject caller)
        {
            _enabled = true;
        }
    }
}
