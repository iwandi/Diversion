using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base.Math
{
    [DataContract]
    public struct Vector2
    {
        [DataMember(Order = 0)]
        float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        [DataMember(Order = 1)]
        float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
