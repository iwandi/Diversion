namespace Diversion.Editor
{
    partial class Editor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inspector = new Diversion.Editor.Inspector();
            this.hirachieView = new Diversion.Editor.Forms.HirachieView();
            this.SuspendLayout();
            // 
            // inspector
            // 
            this.inspector.Location = new System.Drawing.Point(347, 12);
            this.inspector.Name = "inspector";
            this.inspector.SelectedObject = null;
            this.inspector.Size = new System.Drawing.Size(427, 542);
            this.inspector.TabIndex = 1;
            // 
            // hirachieView
            // 
            this.hirachieView.Location = new System.Drawing.Point(12, 12);
            this.hirachieView.Name = "hirachieView";
            this.hirachieView.Size = new System.Drawing.Size(329, 542);
            this.hirachieView.TabIndex = 0;
            this.hirachieView.SelectionChanged += new Diversion.Base.GameObjectParamDelgate(this.hirachieView_SelectionChanged);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 566);
            this.Controls.Add(this.inspector);
            this.Controls.Add(this.hirachieView);
            this.Name = "Editor";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.HirachieView hirachieView;
        private Inspector inspector;
    }
}

