using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diversion.Base
{
    // TODO : Implement SendMessage with Emit
    // TODO : Implement SendMassage with Reflection
    // TODO : Optimize Component Removel so it dose not Loose all Caches
    // TODO : Allow layouts to get maarraged and all Storrage get repartitiont

    [DataContract]
    public class GameObjectLayout
    {
        // TODO : invalid GetComponent always returns null so cahche them together

        // log cache usage so we can cleanup unused parts of the cache 
        Dictionary<Type, ComponentCacheElement> componentCache = new Dictionary<Type, ComponentCacheElement>();
        Dictionary<string, object> massageCallCache = new Dictionary<string, object>();
        Dictionary<string, object> propertyCallCache = new Dictionary<string, object>();

        public readonly static GameObjectLayout EmptyObjectLayout = new GameObjectLayout();

        public GameObjectLayout AddComponent(Type type, ComponentStorrage storrage, out Component comp)
        {
            comp = Factory.CreateInstance<Component>(type);
            storrage.AddComponent(comp,false);
            return Clone();
        }

        public GameObjectLayout RemoveComponent(Component comp, ComponentStorrage storrage)
        {
            GameObjectLayout layout = new GameObjectLayout();
            storrage.RemoveComponent(comp);            
            return layout;
        }

        // TODO : Link Clones so we can jump back to a ould configuration ?
        GameObjectLayout Clone()
        {
            GameObjectLayout clone = new GameObjectLayout();
            clone.componentCache = new Dictionary<Type,ComponentCacheElement>(componentCache);
            clone.massageCallCache = new Dictionary<string, object>(massageCallCache);
            clone.propertyCallCache = new Dictionary<string, object>(propertyCallCache);

            return clone;
        }

        public Component GetComponent(Type type, ComponentStorrage storrage)
        {
            ComponentCacheElement elem;
            if (!componentCache.TryGetValue(type, out elem))
            {
                elem = new ComponentCacheElement(type);
                componentCache.Add(type, elem);
            }
            return elem.GetComponent(storrage);
        }

        public IEnumerator<Component> ListComponent(Type type, ComponentStorrage storrage)
        {
            ComponentCacheElement elem;
            if (!componentCache.TryGetValue(type, out elem))
            {
                elem = new ComponentCacheElement(type);
                componentCache.Add(type, elem);
            }

            return elem.ListComponent(storrage);
        }

        public void SendMessage(string msg, object[] args, ComponentStorrage storrage)
        {
            throw new NotImplementedException();
        }

        public void SetProperty<T>(string name, T value, ComponentStorrage storrage)
        {
            throw new NotImplementedException();
        }

        class ComponentCacheElement
        {
            Type type;
            int startIndex;
            int usage;
            List<int> indexList;

            public ComponentCacheElement(Type type)
            {
                this.type = type;
                startIndex = -1;
                usage = 0;
            }

            public Component GetComponent(ComponentStorrage storrage)
            {
                if (usage < int.MaxValue)
                {
                    usage++;
                }
                Component comp = storrage.GetByIndex(type, startIndex);
                if (comp != null)
                {
                    return comp;
                }
                // index hit was wrong so 
                startIndex = -1;
                comp = storrage.Search(type, out startIndex);
                return comp;
            }

            public IEnumerator<Component> ListComponent(ComponentStorrage storrage)
            {
                int iIndex = 0;
                if (indexList == null)
                {
                    Component comp = storrage.GetByIndex(type, startIndex);
                    if (comp != null)
                    {
                        yield return comp;
                    }
                    if (startIndex + 1 >= storrage.ComponetCount)
                    {
                        indexList = new List<int>(new int[]{startIndex});
                    }
                    else
                    {
                        indexList = new List<int>();
                        indexList.Add(startIndex);
                        int index = startIndex;
                        while (storrage.Search(type, out index, index) != null)
                        {
                            indexList.Add(index);
                        }
                    }
                    iIndex = 1;
                }
                else
                {
                    for (int i = iIndex; i < indexList.Count; i++)
                    {
                        Component comp = storrage.GetByIndex(type, indexList[i]);
                        if (comp != null)
                        {
                            yield return comp;
                        }
                        else
                        {
                            // this should be impossible
                            throw new Exception();
                        }
                    }
                }
            }
        }
    }
}
