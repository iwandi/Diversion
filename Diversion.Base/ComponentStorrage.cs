using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base
{
    [DataContract]
    public class ComponentStorrage
    {
        [DataMember(Order = 0)]
        List<Component> components;

        public int ComponetCount
        {
            get { return components.Count; }
        }

        public ComponentStorrage()
        {
            components = new List<Component>();
        }

        // used for cloning
        ComponentStorrage(IList<Component> compList)
        {
            components = new List<Component>(compList.Count);
            // TODO check if the indexs stays the same !!!
            components.AddRange(compList);
        }

        public void AddComponent(Component comp, bool checkDuplicates = true)
        {
            if (!checkDuplicates || !components.Contains<Component>(comp))
            {
                components.Add(comp);
            }
        }

        public void RemoveComponent(Component comp)
        {
            components.Remove(comp);
        }

        // PERF : Check if GetType() and IsAssignableFrom perform well
        public Component GetByIndex(Type t, int index)
        {
            if (index < 0 || index >= components.Count)
            {
                return null;
            }
            Component comp = components[index];
            Type compType = comp.GetType();
            if (compType.IsAssignableFrom(t))
            {
                return comp;
            }
            return null;
        }

        // PERF : if we store type in a seprate list we can search quicker
        public Component Search(Type t, out int index, int startIndex = 0)
        {
            for (int i = startIndex; i < components.Count; i++)
            {
                Component comp = components[i];
                Type compType = comp.GetType();
                if (compType.IsAssignableFrom(t))
                {
                    index = i;
                    return comp;
                }
            }
            index = -1;
            return null;
        }

        public ComponentStorrage Clone()
        {
            ComponentStorrage clone = new ComponentStorrage(components);
            return clone;
        }
    }
}
