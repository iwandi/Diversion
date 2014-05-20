using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diversion.Base
{
    [Flags]
    public enum GameObjectMessageScope : short
    {
        None = 0,
        Local = 1,
        Children = 2,
        Parents = 4,
        Root = 8,
        Scene = 16,
        Global = 32
    }
}
