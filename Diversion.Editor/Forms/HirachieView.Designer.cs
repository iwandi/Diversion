namespace Diversion.Editor.Forms
{
    partial class HirachieView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HirachieView));
            this.tools = new System.Windows.Forms.ToolStrip();
            this.addGameObject = new System.Windows.Forms.ToolStripButton();
            this.seperator = new System.Windows.Forms.ToolStripSeparator();
            this.filter = new System.Windows.Forms.ToolStripTextBox();
            this.clearFilter = new System.Windows.Forms.ToolStripButton();
            this.view = new System.Windows.Forms.TreeView();
            this.tools.SuspendLayout();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tools.Dock = System.Windows.Forms.DockStyle.None;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGameObject,
            this.seperator,
            this.filter,
            this.clearFilter});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(166, 25);
            this.tools.TabIndex = 0;
            this.tools.Text = "toolStrip1";
            // 
            // addGameObject
            // 
            this.addGameObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addGameObject.Image = ((System.Drawing.Image)(resources.GetObject("addGameObject.Image")));
            this.addGameObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addGameObject.Name = "addGameObject";
            this.addGameObject.Size = new System.Drawing.Size(23, 22);
            this.addGameObject.Text = "addGameObject";
            this.addGameObject.Click += new System.EventHandler(this.addGameObject_Click);
            // 
            // seperator
            // 
            this.seperator.Name = "seperator";
            this.seperator.Size = new System.Drawing.Size(6, 25);
            // 
            // filter
            // 
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(100, 25);
            // 
            // clearFilter
            // 
            this.clearFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearFilter.Image = ((System.Drawing.Image)(resources.GetObject("clearFilter.Image")));
            this.clearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearFilter.Name = "clearFilter";
            this.clearFilter.Size = new System.Drawing.Size(23, 22);
            this.clearFilter.Text = "toolStripButton1";
            // 
            // view
            // 
            this.view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.view.Location = new System.Drawing.Point(0, 25);
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(354, 456);
            this.view.TabIndex = 1;
            this.view.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.view_BeforeExpand);
            this.view.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.view_AfterSelect);
            // 
            // HirachieView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.view);
            this.Controls.Add(this.tools);
            this.Name = "HirachieView";
            this.Size = new System.Drawing.Size(354, 481);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton addGameObject;
        private System.Windows.Forms.ToolStripSeparator seperator;
        private System.Windows.Forms.ToolStripTextBox filter;
        private System.Windows.Forms.ToolStripButton clearFilter;
        private System.Windows.Forms.TreeView view;
    }
}
