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
    public partial class Inspector : UserControl
    {

        object selectedObject;
        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (selectedObject != value)
                {
                    selectedObject = value;
                    UpdateEditor();
                }
            }
        }

        public Inspector()
        {
            InitializeComponent();
        }

        void UpdateEditor()
        {
            CleanInspactorStack();
            InspectorControl control = InspectorControlByType(selectedObject.GetType());
            if (control != null)
            {
                AddInspector(control);
                control.SetTarget(selectedObject, this);
            }
        }

        public void CleanInspactorStack()
        {
            controls.Controls.Clear();
        }

        public void AddInspector(InspectorControl control)
        {
            controls.Controls.Add(control);
        }

        InspectorControl InspectorControlByType(Type type)
        {
            if (type == typeof(GameObject))
            {
                return new GameObjectInspector();
            }
            return null;
        }
    }
}
