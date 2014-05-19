using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base.Math
{
    [DataContract]
    public struct Vector4
    {
        [DataMember(Order=0)]
        float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        [DataMember(Order=1)]
        float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        [DataMember(Order=2)]
        float z;
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        [DataMember(Order=3)]
        float w;
        public float W
        {
            get { return w; }
            set { w = value; }
        }
    }
}
