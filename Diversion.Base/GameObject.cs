using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base
{
    // hirachie onely with transforms ???
    // not multi thread save interation of components
    [DataContract]
    public class GameObject
    {
        GameObjectLayout layout = GameObjectLayout.EmptyObjectLayout;

        [DataMember(Order=0)]
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember(Order=1)]
        bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        [DataMember(Order=2)]
        IList<Component> components = new List<Component>();

        bool m_bound = false;
        bool m_started = false;

        Component InternalAddComponent(Type type)
        {
            Component comp = Activator.CreateInstance(type) as Component;
            comp.gameObject = this;
            components.Add(comp);
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
            for (int i = 0; i < components.Count; i++)
            {
                Component comp = components[i];
                if (comp.GetType().IsAssignableFrom(type))
                {
                    return comp;
                }
            }
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

        IEnumerator<Component> InternalGetComponents(Type type)
        {
            for (int i = 0; i < components.Count; i++)
            {
                Component comp = components[i];
                if (comp.GetType().IsAssignableFrom(type))
                {
                    yield return comp;
                }
            }
        }

        public IEnumerator<Component> GetComponents(Type type)
        {
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new Exception("Cant get Component type that is not based on Component");
            }

            return InternalGetComponents(type);
        }

        public IEnumerator<T> GetComponents<T>() where T : Component
        {
            return InternalGetComponents(typeof(T)) as IEnumerator<T>;
        }

        /*public Component GetComponentInChildren(Type type)
        {
            throw new NotImplementedException();
        }

        public T GetComponentInChildren<T>() where T : Component
        {
            return GetComponentInChildren(typeof(T)) as T;
        }

        public IEnumerator<Component> GetComponentsInChildren(Type type)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetComponentsInChildren<T>() where T : Component
        {
            return GetComponentsInChildren(typeof(T)) as IEnumerator<T>;
        }

        public Component GetComponentInChildrenOfRoot(Type type)
        {
            throw new NotImplementedException();
        }

        public T GetComponentInChildrenOfRoot<T>() where T : Component
        {
            return GetComponentInChildrenOfRoot(typeof(T)) as T;
        }

        public IEnumerator<Component> GetComponentsInChildrenOfRoot(Type type)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetComponentsInChildrenOfRoot<T>() where T : Component
        {
            return GetComponentsInChildrenOfRoot(typeof(T)) as IEnumerator<T>;
        }*/

        public void Bind()
        {
            m_bound = true;
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Bind();
            }

            // TODO find a way to get child objects and call set acive
        }

        public void Reset()
        {
            m_started = true;
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Reset();
            }

            // TODO find a way to get child objects and call set acive
        }

        public void SetActiveRecursively(bool active)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].EnabledLocal = active;
            }

            // TODO find a way to get child objects and call set acive
        }

        public void SendMessage(string msg, MessageScope scope,  params object[] args)
        {
            throw new NotImplementedException();
        }

        [Flags]
        public enum MessageScope : byte
        {
            None = 0,
            Local = 1,
            Children = 2,
            Parents = 4
        }
    }
}
