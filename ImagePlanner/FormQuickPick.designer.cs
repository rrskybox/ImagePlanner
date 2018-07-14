namespace ImagePlanner
{
    partial class FormQuickPick
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuickPick));
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Closebutton = new System.Windows.Forms.Button();
            this.DurationTBMax = new System.Windows.Forms.Label();
            this.DurationTBMin = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.AltitudeTBMax = new System.Windows.Forms.Label();
            this.AltitudeTBMin = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.CatalogedTypesList = new System.Windows.Forms.ListBox();
            this.TargetObjectList = new System.Windows.Forms.ListBox();
            this.SizeTBMax = new System.Windows.Forms.Label();
            this.SizeTBMin = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.TargetClassBox = new System.Windows.Forms.GroupBox();
            this.SelectNebulaButton = new System.Windows.Forms.RadioButton();
            this.SelectClusterButton = new System.Windows.Forms.RadioButton();
            this.SelectGalaxyButton = new System.Windows.Forms.RadioButton();
            this.SizeNumeric = new System.Windows.Forms.NumericUpDown();
            this.AltitudeNumeric = new System.Windows.Forms.NumericUpDown();
            this.DurationNumeric = new System.Windows.Forms.NumericUpDown();
            this.TargetClassBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AltitudeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(320, 11);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(43, 13);
            this.Label9.TabIndex = 68;
            this.Label9.Text = "Targets";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(31, 11);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(70, 13);
            this.Label8.TabIndex = 67;
            this.Label8.Text = "Target Types";
            // 
            // Closebutton
            // 
            this.Closebutton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Closebutton.Location = new System.Drawing.Point(179, 242);
            this.Closebutton.Name = "Closebutton";
            this.Closebutton.Size = new System.Drawing.Size(45, 29);
            this.Closebutton.TabIndex = 66;
            this.Closebutton.Text = "Close";
            this.Closebutton.UseVisualStyleBackColor = true;
            // 
            // DurationTBMax
            // 
            this.DurationTBMax.AutoSize = true;
            this.DurationTBMax.Location = new System.Drawing.Point(240, 207);
            this.DurationTBMax.Name = "DurationTBMax";
            this.DurationTBMax.Size = new System.Drawing.Size(27, 13);
            this.DurationTBMax.TabIndex = 58;
            this.DurationTBMax.Text = "Max";
            // 
            // DurationTBMin
            // 
            this.DurationTBMin.AutoSize = true;
            this.DurationTBMin.Location = new System.Drawing.Point(145, 207);
            this.DurationTBMin.Name = "DurationTBMin";
            this.DurationTBMin.Size = new System.Drawing.Size(24, 13);
            this.DurationTBMin.TabIndex = 57;
            this.DurationTBMin.Text = "Min";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(167, 176);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(77, 26);
            this.Label10.TabIndex = 56;
            this.Label10.Text = "Total Available\r\n(hours)";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AltitudeTBMax
            // 
            this.AltitudeTBMax.AutoSize = true;
            this.AltitudeTBMax.Location = new System.Drawing.Point(241, 139);
            this.AltitudeTBMax.Name = "AltitudeTBMax";
            this.AltitudeTBMax.Size = new System.Drawing.Size(27, 13);
            this.AltitudeTBMax.TabIndex = 54;
            this.AltitudeTBMax.Text = "Max";
            // 
            // AltitudeTBMin
            // 
            this.AltitudeTBMin.AutoSize = true;
            this.AltitudeTBMin.Location = new System.Drawing.Point(146, 139);
            this.AltitudeTBMin.Name = "AltitudeTBMin";
            this.AltitudeTBMin.Size = new System.Drawing.Size(24, 13);
            this.AltitudeTBMin.TabIndex = 53;
            this.AltitudeTBMin.Text = "Min";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(167, 108);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(79, 26);
            this.Label2.TabIndex = 52;
            this.Label2.Text = "Current Altitude\r\n(deg)";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CatalogedTypesList
            // 
            this.CatalogedTypesList.FormattingEnabled = true;
            this.CatalogedTypesList.Location = new System.Drawing.Point(12, 137);
            this.CatalogedTypesList.Name = "CatalogedTypesList";
            this.CatalogedTypesList.Size = new System.Drawing.Size(114, 134);
            this.CatalogedTypesList.Sorted = true;
            this.CatalogedTypesList.TabIndex = 51;
            this.CatalogedTypesList.SelectedIndexChanged += new System.EventHandler(this.CatalogedTypesList_SelectedIndexChanged);
            // 
            // TargetObjectList
            // 
            this.TargetObjectList.FormattingEnabled = true;
            this.TargetObjectList.Location = new System.Drawing.Point(285, 33);
            this.TargetObjectList.Name = "TargetObjectList";
            this.TargetObjectList.Size = new System.Drawing.Size(116, 238);
            this.TargetObjectList.TabIndex = 50;
            this.TargetObjectList.SelectedIndexChanged += new System.EventHandler(this.TargetObjectList_SelectedIndexChanged);
            // 
            // SizeTBMax
            // 
            this.SizeTBMax.AutoSize = true;
            this.SizeTBMax.Location = new System.Drawing.Point(239, 69);
            this.SizeTBMax.Name = "SizeTBMax";
            this.SizeTBMax.Size = new System.Drawing.Size(27, 13);
            this.SizeTBMax.TabIndex = 48;
            this.SizeTBMax.Text = "Max";
            // 
            // SizeTBMin
            // 
            this.SizeTBMin.AutoSize = true;
            this.SizeTBMin.Location = new System.Drawing.Point(144, 69);
            this.SizeTBMin.Name = "SizeTBMin";
            this.SizeTBMin.Size = new System.Drawing.Size(24, 13);
            this.SizeTBMin.TabIndex = 47;
            this.SizeTBMin.Text = "Min";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(175, 38);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(61, 26);
            this.Label3.TabIndex = 46;
            this.Label3.Text = "Object Size\r\n(arcmin)";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TargetClassBox
            // 
            this.TargetClassBox.Controls.Add(this.SelectNebulaButton);
            this.TargetClassBox.Controls.Add(this.SelectClusterButton);
            this.TargetClassBox.Controls.Add(this.SelectGalaxyButton);
            this.TargetClassBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TargetClassBox.Location = new System.Drawing.Point(13, 38);
            this.TargetClassBox.Name = "TargetClassBox";
            this.TargetClassBox.Size = new System.Drawing.Size(112, 93);
            this.TargetClassBox.TabIndex = 69;
            this.TargetClassBox.TabStop = false;
            this.TargetClassBox.Text = "Target Class";
            // 
            // SelectNebulaButton
            // 
            this.SelectNebulaButton.AutoSize = true;
            this.SelectNebulaButton.Location = new System.Drawing.Point(21, 66);
            this.SelectNebulaButton.Name = "SelectNebulaButton";
            this.SelectNebulaButton.Size = new System.Drawing.Size(65, 17);
            this.SelectNebulaButton.TabIndex = 2;
            this.SelectNebulaButton.TabStop = true;
            this.SelectNebulaButton.Text = "Nebulae";
            this.SelectNebulaButton.UseVisualStyleBackColor = true;
            this.SelectNebulaButton.CheckedChanged += new System.EventHandler(this.SelectNebulaButton_CheckedChanged);
            // 
            // SelectClusterButton
            // 
            this.SelectClusterButton.AutoSize = true;
            this.SelectClusterButton.Location = new System.Drawing.Point(21, 43);
            this.SelectClusterButton.Name = "SelectClusterButton";
            this.SelectClusterButton.Size = new System.Drawing.Size(62, 17);
            this.SelectClusterButton.TabIndex = 1;
            this.SelectClusterButton.TabStop = true;
            this.SelectClusterButton.Text = "Clusters";
            this.SelectClusterButton.UseVisualStyleBackColor = true;
            this.SelectClusterButton.CheckedChanged += new System.EventHandler(this.SelectClusterButton_CheckedChanged);
            // 
            // SelectGalaxyButton
            // 
            this.SelectGalaxyButton.AutoSize = true;
            this.SelectGalaxyButton.Location = new System.Drawing.Point(21, 20);
            this.SelectGalaxyButton.Name = "SelectGalaxyButton";
            this.SelectGalaxyButton.Size = new System.Drawing.Size(65, 17);
            this.SelectGalaxyButton.TabIndex = 0;
            this.SelectGalaxyButton.TabStop = true;
            this.SelectGalaxyButton.Text = "Galaxies";
            this.SelectGalaxyButton.UseVisualStyleBackColor = true;
            this.SelectGalaxyButton.CheckedChanged += new System.EventHandler(this.SelectGalaxyButton_CheckedChanged);
            // 
            // SizeNumeric
            // 
            this.SizeNumeric.Location = new System.Drawing.Point(176, 67);
            this.SizeNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SizeNumeric.Name = "SizeNumeric";
            this.SizeNumeric.Size = new System.Drawing.Size(58, 20);
            this.SizeNumeric.TabIndex = 70;
            this.SizeNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SizeNumeric.ValueChanged += new System.EventHandler(this.SizeNumeric_ValueChanged);
            // 
            // AltitudeNumeric
            // 
            this.AltitudeNumeric.Location = new System.Drawing.Point(177, 137);
            this.AltitudeNumeric.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.AltitudeNumeric.Name = "AltitudeNumeric";
            this.AltitudeNumeric.Size = new System.Drawing.Size(58, 20);
            this.AltitudeNumeric.TabIndex = 71;
            this.AltitudeNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AltitudeNumeric.ValueChanged += new System.EventHandler(this.AltitudeNumeric_ValueChanged);
            // 
            // DurationNumeric
            // 
            this.DurationNumeric.DecimalPlaces = 1;
            this.DurationNumeric.Location = new System.Drawing.Point(176, 205);
            this.DurationNumeric.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.DurationNumeric.Name = "DurationNumeric";
            this.DurationNumeric.Size = new System.Drawing.Size(58, 20);
            this.DurationNumeric.TabIndex = 72;
            this.DurationNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DurationNumeric.ValueChanged += new System.EventHandler(this.DurationNumeric_ValueChanged);
            // 
            // FormQuickPick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(413, 290);
            this.ControlBox = false;
            this.Controls.Add(this.DurationNumeric);
            this.Controls.Add(this.AltitudeNumeric);
            this.Controls.Add(this.SizeNumeric);
            this.Controls.Add(this.TargetClassBox);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Closebutton);
            this.Controls.Add(this.DurationTBMax);
            this.Controls.Add(this.DurationTBMin);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.AltitudeTBMax);
            this.Controls.Add(this.AltitudeTBMin);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.CatalogedTypesList);
            this.Controls.Add(this.TargetObjectList);
            this.Controls.Add(this.SizeTBMax);
            this.Controls.Add(this.SizeTBMin);
            this.Controls.Add(this.Label3);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormQuickPick";
            this.Text = "Quick Pick";
            this.TopMost = true;
            this.TargetClassBox.ResumeLayout(false);
            this.TargetClassBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AltitudeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Button Closebutton;
        internal System.Windows.Forms.Label DurationTBMax;
        internal System.Windows.Forms.Label DurationTBMin;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label AltitudeTBMax;
        internal System.Windows.Forms.Label AltitudeTBMin;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ListBox CatalogedTypesList;
        internal System.Windows.Forms.ListBox TargetObjectList;
        internal System.Windows.Forms.Label SizeTBMax;
        internal System.Windows.Forms.Label SizeTBMin;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.GroupBox TargetClassBox;
        private System.Windows.Forms.RadioButton SelectNebulaButton;
        private System.Windows.Forms.RadioButton SelectClusterButton;
        private System.Windows.Forms.RadioButton SelectGalaxyButton;
        private System.Windows.Forms.NumericUpDown SizeNumeric;
        private System.Windows.Forms.NumericUpDown AltitudeNumeric;
        private System.Windows.Forms.NumericUpDown DurationNumeric;
    }
}

