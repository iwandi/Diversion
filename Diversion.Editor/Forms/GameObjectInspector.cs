using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Diversion.Base;

namespace Diversion.Editor
{
    public partial class GameObjectInspector : InspectorControl
    {
        GameObject gameObject;

        public GameObjectInspector()
        {
            InitializeComponent();
        }

        public override void SetTarget(object target, Inspector host)
        {
            gameObject = target as GameObject;
            if (gameObject != null)
            {
                name.Text = gameObject.Name;
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            gameObject.Name = name.Text;
        }
    }
}
