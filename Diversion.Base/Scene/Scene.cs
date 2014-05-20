using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diversion.Base.Scene
{
    public class Scene
    {
        public event GameObjectParamDelgate OnGameObjectAdded; 

        List<GameObject> rootGameObjects = new List<GameObject>();
        public System.Collections.ObjectModel.ReadOnlyCollection<GameObject> Children { get { return rootGameObjects.AsReadOnly(); } }
        
        public void Add(GameObject gameObject)
        {
            if (gameObject.Parent != null)
            {
                throw new Exception("Only gameobjects withoout a parent are allowed to be added to a scene");
            }
            rootGameObjects.Add(gameObject);
            if ( OnGameObjectAdded != null )
            {
                OnGameObjectAdded(gameObject);
            }
        }
    }
}
