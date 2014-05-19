using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base
{
    public class Transform : Component
    {
        [DataMember(Order=2)]
        bool isStatic;
        public bool IsStatic
        {
            get { return isStatic; }
            set { isStatic = value; }
        }

        [DataMember(Order = 3)]
        Transform root;
        public Transform Root { get { return root; } }

        [DataMember(Order = 4)]
        Transform parent;
        public Transform Parent { get { return parent; } }


        [DataMember(Order = 5)]
        Vector3 localPostion;
        [DataMember(Order = 6)]
        Vector3 localScale;
        [DataMember(Order = 7)]
        Quaternion localRotation;

        [DataMember(Order = 8)]
        protected IList<Transform> children = new List<Transform>();
        
        public Vector3 LocalPosition
        {
            get { return localPostion; }
            set { localPostion = value; }
        }

        public Vector3 LocalScale
        {
            get { return localScale; }
            set { localScale = value; }
        }

        public Quaternion LocalRotation
        {
            get { return localRotation; }
            set { localRotation = value; }
        }
        
        // TODO : Do we need to check it from the parrent ?
        public Vector3 WorldPosition
        {
            get { return LocalToWorld(LocalPosition); }
            set { localPostion = WorldToLocal(value); }
        }

        // TODO : Do we need to check it from the parrent ?
        public Quaternion WorldRotation
        {
            get { return LocalToWorld(localRotation); }
            set { localRotation = WorldToLocal(localRotation); }
        }

        public Vector3 WorldForward
        {
            get { return LocalToWorld(Vector3.Forward); }
        }

        public Vector3 WorldBackword
        {
            get { return LocalToWorld(Vector3.Backward); }
        }

        public Vector3 WorldRight
        {
            get { return LocalToWorld(Vector3.Right); }
        }

        public Vector3 WorldLeft
        {
            get { return LocalToWorld(Vector3.Left); }
        }

        public Vector3 WorldUp
        {
            get { return LocalToWorld(Vector3.Up); }
        }

        public Vector3 WorldDown
        {
            get { return LocalToWorld(Vector3.Down); }
        }

        public Vector3 LocalToWorld(Vector3 pos)
        {
            throw new NotImplementedException();
        }

        public Quaternion LocalToWorld(Quaternion rot)
        {
            throw new NotImplementedException();
        }

        public Vector3 WorldToLocal(Vector3 pos)
        {
            throw new NotImplementedException();
        }

        public Quaternion WorldToLocal(Quaternion rot)
        {
            throw new NotImplementedException();
        }
    }
}
