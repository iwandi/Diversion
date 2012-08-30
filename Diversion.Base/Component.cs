using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diversion.Base
{
    public abstract class Component
    {
        internal protected GameObject gameObject;
        public GameObject GameObject { get { return gameObject; } }

        protected bool enabled;
        public bool Enabled 
        { 
            get { return enabled; }
            virtual set { enabled = value; }
        }

        // Time to Bind to other Component
        public virtual void Bind()
        {

        }

        // Serialisation is done now do stuff
        public virtual void Reset()
        {

        }

        // we always need a way to clean up
        public virtual void Destroy()
        {

        }
    }
}
