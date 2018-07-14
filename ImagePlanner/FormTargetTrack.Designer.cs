namespace ImagePlanner
{
    partial class FormTargetTrack
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
            this.TrackPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // TrackPanel
            // 
            this.TrackPanel.BackColor = System.Drawing.Color.MidnightBlue;
            this.TrackPanel.ForeColor = System.Drawing.Color.Coral;
            this.TrackPanel.Location = new System.Drawing.Point(-1, 2);
            this.TrackPanel.Name = "TrackPanel";
            this.TrackPanel.Size = new System.Drawing.Size(250, 250);
            this.TrackPanel.TabIndex = 0;
            // 
            // FormTargetTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(250, 251);
            this.Controls.Add(this.TrackPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTargetTrack";
            this.ShowIcon = false;
            this.Text = "Target Track";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TrackPanel;
    }
}