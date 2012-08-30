using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diversion.Base.Rendering
{
    public class RenderManager
    {
        IList<IRenderer> renders = new List<IRenderer>();

        internal void RegisterRender(IRenderer render)
        {
            renders.Add(render);
        }

        internal void UnRegisterRender(IRenderer render)
        {
            renders.Remove(render);
        }

        void Exec()
        {
            
        }
    }
}
