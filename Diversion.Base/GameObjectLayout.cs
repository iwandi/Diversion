using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diversion.Base
{
    public class GameObjectLayout
    {
        public readonly static GameObjectLayout EmptyObjectLayout = new GameObjectLayout();

        public GameObjectLayout AddComponent(Type type, out Component comp)
        {
            throw new NotImplementedException();
        }

        public Component GetComponent(Type type, ComponentStorrage storrage)
        {
            throw new NotImplementedException();
        }
    }
}
