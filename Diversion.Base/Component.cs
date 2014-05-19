using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base
{
    [DataContract]
    public abstract class Component
    {
        [DataMember(Order=0)]
        internal protected GameObject gameObject;
        public GameObject GameObject { get { return gameObject; } }

        public bool Enabled 
        { 
            get { return enabledLocal && gameObject.Enabled; }
        }

        [DataMember(Order=1)]
        bool enabledLocal;
        public bool EnabledLocal
        {
            get { return enabledLocal; }
            set { enabledLocal = value; }
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
