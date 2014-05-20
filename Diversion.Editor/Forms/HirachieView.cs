using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Diversion.Base;
using Diversion.Base.Scene;

namespace Diversion.Editor.Forms
{
    public partial class HirachieView : UserControl
    {
        public event GameObjectParamDelgate SelectionChanged;

        Scene scene = new Scene();

        public HirachieView()
        {
            GameObject.SendMassageEnabled = false;

            InitializeComponent();
        }

        private void addGameObject_Click(object sender, EventArgs e)
        {
            GameObject selectedObject = null;
            if ( view.SelectedNode != null )
            {
                selectedObject = view.SelectedNode.Tag as GameObject;
            }

            GameObject obj = new GameObject();
            if (selectedObject != null)
            {
                obj.Parent = selectedObject;

                TreeNode node = view.SelectedNode;
                if (node.Nodes.Count <= 0)
                {
                    node.Nodes.Add(new TreeNode());
                }
                node.Expand();
                UpdateView(view.SelectedNode);
            }
            else
            {
                scene.Add(obj);
                UpdateView(null);
            }
        }

        void UpdateView(TreeNode node, bool ignoreExpand = false)
        {
            TreeNodeCollection nodes;
            System.Collections.ObjectModel.ReadOnlyCollection<GameObject> children;
            if (node == null)
            {
                nodes = view.Nodes;
                children = scene.Children;
            }
            else
            {
                // update the node itself
                nodes = node.Nodes;
                children = ((GameObject)node.Tag).Children;
                bool hasChildren = children.Count > 0;
                bool hasNodes = nodes.Count > 0;

                GameObject obj = node.Tag as GameObject;
                if ( obj != null )
                {
                    node.Name = obj.Name;                    
                }

                if (hasChildren && !hasNodes && !node.IsExpanded)
                {
                    nodes.Add(new TreeNode());
                }

                if (!node.IsExpanded && !ignoreExpand)
                {
                    return;
                }
            }

            foreach (GameObject child in children)
            {
                TreeNode childNode;
                if (TryGetNodeByTag(nodes, child, out childNode))
                {
                    UpdateView(childNode);
                }
                else
                {
                    childNode = new TreeNode(child.Name);
                    childNode.Tag = child;
                    nodes.Add(childNode);
                }
            }

            List<TreeNode> delNodes = new List<TreeNode>();

            foreach (TreeNode searchNode in nodes)
            {
                bool hasGameObject = false;
                foreach (GameObject child in children)
                {
                    if (child == searchNode.Tag)
                    {
                        hasGameObject = true;
                        break;
                    }
                }
                if (!hasGameObject)
                {
                    delNodes.Add(searchNode);
                }
            }

            foreach (TreeNode delNode in delNodes)
            {
                nodes.Remove(delNode);
            }
        }

        bool TryGetNodeByTag(TreeNodeCollection nodes,  object Tag, out TreeNode node)
        {
            foreach (TreeNode searchNode in nodes)
            {
                if (searchNode.Tag == Tag)
                {
                    node = searchNode;
                    return true;
                }
            }
            node = null;
            return false;
        }

        private void view_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            UpdateView(e.Node, true);          
        }

        private void view_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(e.Node.Tag as GameObject);
            }
        }
    }
}
