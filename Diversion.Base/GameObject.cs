using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base
{
    public delegate void GameObjectParamDelgate(GameObject obj);
    public delegate GameObject GameObjectResaultDelegate();

    // not multi thread save interation of components
    [DataContract]
    public class GameObject
    {
        public static bool SendMassageEnabled = true;

        GameObjectLayout layout = GameObjectLayout.EmptyObjectLayout;
        [DataMember(Order = 2)]
        ComponentStorrage storrage = new ComponentStorrage();

        [DataMember(Order = 0)]
        string name = "New GameObject";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember(Order = 1)]
        bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        bool m_bound = false;
        bool m_started = false;

        GameObject parent;
        public GameObject Parent
        {
            get { return parent; }
            set 
            { 
                // TODO : add some child lost event ?
                if (parent != null)
                {
                    parent.children.Remove(this);
                }
                parent = value;
                if (parent != null)
                {
                    parent.children.Add(this);
                }
                // TODO : notifiy parents
                SendMessage("OnParentChanged", GameObjectMessageScope.Local);
                // TODO : send some Into to Children ?
            }
        }

        // PERF : recusive
        public GameObject Root
        {
            get
            {
                if (parent == null)
                {
                    return this;
                }
                return parent.Parent;
            }
        }

        List<GameObject> children = new List<GameObject>();
        public System.Collections.ObjectModel.ReadOnlyCollection<GameObject> Children { get { return children.AsReadOnly(); } }

        Component InternalAddComponent(Type type)
        {
            Component comp;
            layout = layout.AddComponent(type, storrage, out comp);
            comp.gameObject = this;
            
            if (m_bound)
            {
                comp.Bind();
            }
            if (m_started)
            {
                comp.Reset();
            }
            return comp;
        }

        public Component AddComponent(Type type)
        {
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new Exception("Cant Add Component type that is not based on Component");
            }

            return InternalAddComponent(type);
        }

        public void AddComponent<T>() where T : Component
        {
            InternalAddComponent(typeof(T));
        }

        Component InternalGetComponent(Type type)
        {
            return layout.GetComponent(type, storrage);
            throw new KeyNotFoundException();
        }
        
        public Component GetComponent(Type type)
        {
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new Exception("Cant get Component type that is not based on Component");
            }

            return InternalGetComponent(type);
        }

        public T GetComponent<T>() where T : Component
        {
            return InternalGetComponent(typeof(T)) as T;
        }

        IEnumerator<Component> InternalListComponent(Type type)
        {
            return layout.ListComponent(type, storrage);
        }

        public IEnumerator<Component> ListComponent(Type type)
        {
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new Exception("Cant get Component type that is not based on Component");
            }

            return InternalListComponent(type);
        }

        public IEnumerator<T> ListComponent<T>() where T : Component
        {
            return InternalListComponent(typeof(T)) as IEnumerator<T>;
        }

        public void Bind()
        {
            m_bound = true;
            SendMessage("Bind", GameObjectMessageScope.Local | GameObjectMessageScope.Children);
        }

        public void Reset()
        {
            m_started = true;
            SendMessage("Reset", GameObjectMessageScope.Local | GameObjectMessageScope.Children);
        }

        public void SetActiveRecursively(bool active)
        {
            SetProperty<bool>("EnabledLocal", GameObjectMessageScope.Local | GameObjectMessageScope.Children, active);
        }

        public void SendMessage(string msg, GameObjectMessageScope scope, params object[] args)
        {
            InternalSendMessage(msg, scope, args);
        }

        void InternalSendMessage(string msg, GameObjectMessageScope scope, object[] args)
        {
            if (!SendMassageEnabled)
            {
                return;
            }

            bool callGloabl = (scope & GameObjectMessageScope.Global) == GameObjectMessageScope.Global;
            bool callScene = (scope & GameObjectMessageScope.Scene) == GameObjectMessageScope.Scene;
            if (callGloabl || callScene)
            {
                if (callGloabl)
                {
                    throw new NotImplementedException();
                }
                else if (callScene)
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                bool callParents = (scope & GameObjectMessageScope.Parents) == GameObjectMessageScope.Parents;

                if ((scope & GameObjectMessageScope.Root) == GameObjectMessageScope.Root)
                {
                    // ignore this if we are going to call all parents anyways
                    if (!callParents)
                    {
                        Root.InternalSendMessage(msg, GameObjectMessageScope.Local, args);
                    }
                }
                if ((scope & GameObjectMessageScope.Local) == GameObjectMessageScope.Local)
                {
                    layout.SendMessage(msg, args, storrage);
                }
                if (callParents)
                {
                    if (parent != null)
                    {
                        parent.InternalSendMessage(msg, GameObjectMessageScope.Parents, args);
                    }
                }
                if ((scope & GameObjectMessageScope.Children) == GameObjectMessageScope.Children)
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        children[i].InternalSendMessage(msg, GameObjectMessageScope.Children, args);
                    }
                }
            }
        }

        public void SetProperty<T>(string name, GameObjectMessageScope scope, T value)
        {
            if (!SendMassageEnabled)
            {
                return;
            }

            bool callGloabl = (scope & GameObjectMessageScope.Global) == GameObjectMessageScope.Global;
            bool callScene = (scope & GameObjectMessageScope.Scene) == GameObjectMessageScope.Scene;
            if (callGloabl || callScene)
            {
                if (callGloabl)
                {
                    throw new NotImplementedException();
                }
                else if (callScene)
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                bool callParents = (scope & GameObjectMessageScope.Parents) == GameObjectMessageScope.Parents;

                if ((scope & GameObjectMessageScope.Root) == GameObjectMessageScope.Root)
                {
                    // ignore this if we are going to call all parents anyways
                    if (!callParents)
                    {
                        Root.SetProperty<T>(name, GameObjectMessageScope.Local, value);
                    }
                }
                if ((scope & GameObjectMessageScope.Local) == GameObjectMessageScope.Local)
                {
                    layout.SetProperty<T>(name, value, storrage);
                }
                if (callParents)
                {
                    if (parent != null)
                    {
                        parent.SetProperty<T>(name, GameObjectMessageScope.Parents, value);
                    }
                }
                if ((scope & GameObjectMessageScope.Children) == GameObjectMessageScope.Children)
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        children[i].SetProperty<T>(name, GameObjectMessageScope.Children, value);
                    }
                }
            }
        }

        public GameObject Clone()
        {
            GameObject clone = new GameObject();
            clone.layout = layout;
            clone.storrage = storrage.Clone();
            clone.name = name + "(Clone)";
            clone.enabled = enabled;

            // TODO : check bind and reset behavior

            return clone;
        }
    }
}
