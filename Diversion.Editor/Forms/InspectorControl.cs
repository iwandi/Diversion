using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diversion.Editor
{
    public partial class InspectorControl : UserControl
    {
        public InspectorControl()
        {
            InitializeComponent();
        }

        public virtual void SetTarget(object target,Inspector host)
        {

        }
    }
}
