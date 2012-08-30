using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diversion.Base
{
    public sealed class Transform : Component
    {
        protected Transform root;
        public Transform Root { get { return root; } }

        protected Transform parent;
        public Transform Parent { get { return parent; } }

        protected Vector3 localPostion;
        protected Vector3 localScale;
        protected Quaternion localRotation;

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

        public Vector3 WorldPosition
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Vector3 WorldScale
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Quaternion WorldRotation
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Vector3 Forward
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Vector3 Right
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Vector3 Up
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
