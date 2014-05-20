using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base
{
    [DataContract]
    public struct Vector3
    {
        public readonly static Vector3 Forward = new Vector3(1f,0f,0f);
        public readonly static Vector3 Backward = new Vector3(-1f, 0f, 0f);
        public readonly static Vector3 Right = new Vector3(1f, 0f, 0f);
        public readonly static Vector3 Left = new Vector3(-1f, 0f, 0f);
        public readonly static Vector3 Up = new Vector3(0f, 0f, 1f);
        public readonly static Vector3 Down = new Vector3(0f, 0f, -1f);

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

        [DataMember(Order = 2)]
        float z;
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
