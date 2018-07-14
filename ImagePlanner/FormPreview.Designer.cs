namespace ImagePlanner
{
    partial class FormPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreview));
            this.WebBrowserFrame = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // WebBrowserFrame
            // 
            this.WebBrowserFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserFrame.Location = new System.Drawing.Point(0, 0);
            this.WebBrowserFrame.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserFrame.Name = "WebBrowserFrame";
            this.WebBrowserFrame.ScrollBarsEnabled = false;
            this.WebBrowserFrame.Size = new System.Drawing.Size(413, 437);
            this.WebBrowserFrame.TabIndex = 1;
            this.WebBrowserFrame.WebBrowserShortcutsEnabled = false;
            // 
            // FormPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 437);
            this.Controls.Add(this.WebBrowserFrame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreview";
            this.ShowIcon = false;
            this.Text = "FormPreview";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.WebBrowser WebBrowserFrame;
    }
}