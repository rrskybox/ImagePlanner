﻿namespace ImagePlanner
{
    partial class FormProspect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProspect));
            this.DurationNumeric = new System.Windows.Forms.NumericUpDown();
            this.SizeNumeric = new System.Windows.Forms.NumericUpDown();
            this.SelectNebulaButton = new System.Windows.Forms.RadioButton();
            this.SelectClusterButton = new System.Windows.Forms.RadioButton();
            this.SelectGalaxyButton = new System.Windows.Forms.RadioButton();
            this.AltitudeNumeric = new System.Windows.Forms.NumericUpDown();
            this.TargetClassBox = new System.Windows.Forms.GroupBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Closebutton = new System.Windows.Forms.Button();
            this.DurationTBMax = new System.Windows.Forms.Label();
            this.DurationTBMin = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.AltitudeTBMax = new System.Windows.Forms.Label();
            this.AltitudeTBMin = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.CatalogedTypesList = new System.Windows.Forms.ListBox();
            this.SizeTBMax = new System.Windows.Forms.Label();
            this.SizeTBMin = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.ProspectGrid = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DurationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AltitudeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DurationNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AltitudeNumeric)).BeginInit();
            this.TargetClassBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProspectGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DurationNumeric
            // 
            this.DurationNumeric.DecimalPlaces = 1;
            this.DurationNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DurationNumeric.Location = new System.Drawing.Point(167, 192);
            this.DurationNumeric.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.DurationNumeric.Name = "DurationNumeric";
            this.DurationNumeric.Size = new System.Drawing.Size(46, 20);
            this.DurationNumeric.TabIndex = 90;
            this.DurationNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DurationNumeric.ValueChanged += new System.EventHandler(this.DurationNumeric_ValueChanged);
            // 
            // SizeNumeric
            // 
            this.SizeNumeric.Location = new System.Drawing.Point(168, 43);
            this.SizeNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SizeNumeric.Name = "SizeNumeric";
            this.SizeNumeric.Size = new System.Drawing.Size(46, 20);
            this.SizeNumeric.TabIndex = 88;
            this.SizeNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SizeNumeric.ValueChanged += new System.EventHandler(this.SizeNumeric_ValueChanged);
            // 
            // SelectNebulaButton
            // 
            this.SelectNebulaButton.AutoSize = true;
            this.SelectNebulaButton.Location = new System.Drawing.Point(21, 19);
            this.SelectNebulaButton.Name = "SelectNebulaButton";
            this.SelectNebulaButton.Size = new System.Drawing.Size(65, 17);
            this.SelectNebulaButton.TabIndex = 2;
            this.SelectNebulaButton.Text = "Nebulae";
            this.SelectNebulaButton.UseVisualStyleBackColor = true;
            this.SelectNebulaButton.CheckedChanged += new System.EventHandler(this.SelectNebulaButton_CheckedChanged);
            // 
            // SelectClusterButton
            // 
            this.SelectClusterButton.AutoSize = true;
            this.SelectClusterButton.Location = new System.Drawing.Point(21, 70);
            this.SelectClusterButton.Name = "SelectClusterButton";
            this.SelectClusterButton.Size = new System.Drawing.Size(62, 17);
            this.SelectClusterButton.TabIndex = 1;
            this.SelectClusterButton.Text = "Clusters";
            this.SelectClusterButton.UseVisualStyleBackColor = true;
            this.SelectClusterButton.CheckedChanged += new System.EventHandler(this.SelectClusterButton_CheckedChanged);
            // 
            // SelectGalaxyButton
            // 
            this.SelectGalaxyButton.AutoSize = true;
            this.SelectGalaxyButton.Location = new System.Drawing.Point(21, 43);
            this.SelectGalaxyButton.Name = "SelectGalaxyButton";
            this.SelectGalaxyButton.Size = new System.Drawing.Size(65, 17);
            this.SelectGalaxyButton.TabIndex = 0;
            this.SelectGalaxyButton.Text = "Galaxies";
            this.SelectGalaxyButton.UseVisualStyleBackColor = true;
            this.SelectGalaxyButton.CheckedChanged += new System.EventHandler(this.SelectGalaxyButton_CheckedChanged);
            // 
            // AltitudeNumeric
            // 
            this.AltitudeNumeric.Location = new System.Drawing.Point(168, 114);
            this.AltitudeNumeric.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.AltitudeNumeric.Name = "AltitudeNumeric";
            this.AltitudeNumeric.Size = new System.Drawing.Size(46, 20);
            this.AltitudeNumeric.TabIndex = 89;
            this.AltitudeNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AltitudeNumeric.ValueChanged += new System.EventHandler(this.AltitudeNumeric_ValueChanged);
            // 
            // TargetClassBox
            // 
            this.TargetClassBox.Controls.Add(this.SelectNebulaButton);
            this.TargetClassBox.Controls.Add(this.SelectClusterButton);
            this.TargetClassBox.Controls.Add(this.SelectGalaxyButton);
            this.TargetClassBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TargetClassBox.Location = new System.Drawing.Point(11, 39);
            this.TargetClassBox.Name = "TargetClassBox";
            this.TargetClassBox.Size = new System.Drawing.Size(112, 93);
            this.TargetClassBox.TabIndex = 87;
            this.TargetClassBox.TabStop = false;
            this.TargetClassBox.Text = "Target Class";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label8.Location = new System.Drawing.Point(29, 12);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(70, 13);
            this.Label8.TabIndex = 85;
            this.Label8.Text = "Target Types";
            // 
            // Closebutton
            // 
            this.Closebutton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Closebutton.Location = new System.Drawing.Point(168, 242);
            this.Closebutton.Name = "Closebutton";
            this.Closebutton.Size = new System.Drawing.Size(45, 29);
            this.Closebutton.TabIndex = 84;
            this.Closebutton.Text = "Close";
            this.Closebutton.UseVisualStyleBackColor = true;
            this.Closebutton.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // DurationTBMax
            // 
            this.DurationTBMax.AutoSize = true;
            this.DurationTBMax.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DurationTBMax.Location = new System.Drawing.Point(219, 194);
            this.DurationTBMax.Name = "DurationTBMax";
            this.DurationTBMax.Size = new System.Drawing.Size(27, 13);
            this.DurationTBMax.TabIndex = 83;
            this.DurationTBMax.Text = "Max";
            // 
            // DurationTBMin
            // 
            this.DurationTBMin.AutoSize = true;
            this.DurationTBMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DurationTBMin.Location = new System.Drawing.Point(136, 194);
            this.DurationTBMin.Name = "DurationTBMin";
            this.DurationTBMin.Size = new System.Drawing.Size(24, 13);
            this.DurationTBMin.TabIndex = 82;
            this.DurationTBMin.Text = "Min";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label10.Location = new System.Drawing.Point(144, 163);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(91, 26);
            this.Label10.TabIndex = 81;
            this.Label10.Text = "Minimum Duration\r\n(Hours)";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AltitudeTBMax
            // 
            this.AltitudeTBMax.AutoSize = true;
            this.AltitudeTBMax.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AltitudeTBMax.Location = new System.Drawing.Point(220, 116);
            this.AltitudeTBMax.Name = "AltitudeTBMax";
            this.AltitudeTBMax.Size = new System.Drawing.Size(27, 13);
            this.AltitudeTBMax.TabIndex = 80;
            this.AltitudeTBMax.Text = "Max";
            // 
            // AltitudeTBMin
            // 
            this.AltitudeTBMin.AutoSize = true;
            this.AltitudeTBMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AltitudeTBMin.Location = new System.Drawing.Point(137, 116);
            this.AltitudeTBMin.Name = "AltitudeTBMin";
            this.AltitudeTBMin.Size = new System.Drawing.Size(24, 13);
            this.AltitudeTBMin.TabIndex = 79;
            this.AltitudeTBMin.Text = "Min";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label2.Location = new System.Drawing.Point(146, 85);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(86, 26);
            this.Label2.TabIndex = 78;
            this.Label2.Text = "Minimum Altitude\r\n(deg)";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CatalogedTypesList
            // 
            this.CatalogedTypesList.FormattingEnabled = true;
            this.CatalogedTypesList.Location = new System.Drawing.Point(10, 138);
            this.CatalogedTypesList.Name = "CatalogedTypesList";
            this.CatalogedTypesList.Size = new System.Drawing.Size(114, 134);
            this.CatalogedTypesList.Sorted = true;
            this.CatalogedTypesList.TabIndex = 77;
            this.CatalogedTypesList.DoubleClick += new System.EventHandler(this.CatalogedTypesList_DoubleClickEvent);
            // 
            // SizeTBMax
            // 
            this.SizeTBMax.AutoSize = true;
            this.SizeTBMax.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SizeTBMax.Location = new System.Drawing.Point(219, 45);
            this.SizeTBMax.Name = "SizeTBMax";
            this.SizeTBMax.Size = new System.Drawing.Size(27, 13);
            this.SizeTBMax.TabIndex = 75;
            this.SizeTBMax.Text = "Max";
            // 
            // SizeTBMin
            // 
            this.SizeTBMin.AutoSize = true;
            this.SizeTBMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SizeTBMin.Location = new System.Drawing.Point(136, 45);
            this.SizeTBMin.Name = "SizeTBMin";
            this.SizeTBMin.Size = new System.Drawing.Size(24, 13);
            this.SizeTBMin.TabIndex = 74;
            this.SizeTBMin.Text = "Min";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label3.Location = new System.Drawing.Point(154, 12);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(71, 26);
            this.Label3.TabIndex = 73;
            this.Label3.Text = "Minimum Size\r\n(arcmin)";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProspectGrid
            // 
            this.ProspectGrid.AllowUserToAddRows = false;
            this.ProspectGrid.AllowUserToDeleteRows = false;
            this.ProspectGrid.AllowUserToResizeColumns = false;
            this.ProspectGrid.AllowUserToResizeRows = false;
            this.ProspectGrid.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.ProspectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ProspectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.SizeColumn,
            this.DurationColumn,
            this.AltitudeColumn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProspectGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.ProspectGrid.Location = new System.Drawing.Point(262, 14);
            this.ProspectGrid.MultiSelect = false;
            this.ProspectGrid.Name = "ProspectGrid";
            this.ProspectGrid.ReadOnly = true;
            this.ProspectGrid.RowHeadersVisible = false;
            this.ProspectGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ProspectGrid.Size = new System.Drawing.Size(302, 258);
            this.ProspectGrid.TabIndex = 91;
            this.ProspectGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProspectGrid_CellDoubleClickEvent);
            // 
            // NameColumn
            // 
            dataGridViewCellStyle1.NullValue = null;
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.NameColumn.HeaderText = "Object Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // SizeColumn
            // 
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.SizeColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.SizeColumn.HeaderText = "Size (ArcMin)";
            this.SizeColumn.Name = "SizeColumn";
            this.SizeColumn.ReadOnly = true;
            this.SizeColumn.Width = 60;
            // 
            // DurationColumn
            // 
            dataGridViewCellStyle3.Format = "N1";
            dataGridViewCellStyle3.NullValue = null;
            this.DurationColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.DurationColumn.HeaderText = "Duration (hh:mm)";
            this.DurationColumn.Name = "DurationColumn";
            this.DurationColumn.ReadOnly = true;
            this.DurationColumn.Width = 60;
            // 
            // AltitudeColumn
            // 
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.AltitudeColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.AltitudeColumn.HeaderText = "Maximum Altitude";
            this.AltitudeColumn.Name = "AltitudeColumn";
            this.AltitudeColumn.ReadOnly = true;
            this.AltitudeColumn.Width = 60;
            // 
            // FormWazzup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(572, 282);
            this.Controls.Add(this.ProspectGrid);
            this.Controls.Add(this.DurationNumeric);
            this.Controls.Add(this.SizeNumeric);
            this.Controls.Add(this.AltitudeNumeric);
            this.Controls.Add(this.TargetClassBox);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Closebutton);
            this.Controls.Add(this.DurationTBMax);
            this.Controls.Add(this.DurationTBMin);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.AltitudeTBMax);
            this.Controls.Add(this.AltitudeTBMin);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.CatalogedTypesList);
            this.Controls.Add(this.SizeTBMax);
            this.Controls.Add(this.SizeTBMin);
            this.Controls.Add(this.Label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWazzup";
            this.ShowIcon = false;
            this.Text = "WazzUp";
            ((System.ComponentModel.ISupportInitialize)(this.DurationNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AltitudeNumeric)).EndInit();
            this.TargetClassBox.ResumeLayout(false);
            this.TargetClassBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProspectGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown DurationNumeric;
        private System.Windows.Forms.NumericUpDown SizeNumeric;
        private System.Windows.Forms.RadioButton SelectNebulaButton;
        private System.Windows.Forms.RadioButton SelectClusterButton;
        private System.Windows.Forms.RadioButton SelectGalaxyButton;
        private System.Windows.Forms.NumericUpDown AltitudeNumeric;
        private System.Windows.Forms.GroupBox TargetClassBox;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Button Closebutton;
        internal System.Windows.Forms.Label DurationTBMax;
        internal System.Windows.Forms.Label DurationTBMin;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label AltitudeTBMax;
        internal System.Windows.Forms.Label AltitudeTBMin;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ListBox CatalogedTypesList;
        internal System.Windows.Forms.Label SizeTBMax;
        internal System.Windows.Forms.Label SizeTBMin;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.DataGridView ProspectGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DurationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AltitudeColumn;
    }
}