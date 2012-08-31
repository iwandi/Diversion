using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diversion.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireComponentAttribute : Attribute
    {
        Type type;
        public Type Type { get { return type; } }

        public RequireComponentAttribute(Type type)
        {
            this.type = type;
        }
    }
}
