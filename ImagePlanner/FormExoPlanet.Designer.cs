namespace ImagePlanner
{
    partial class FormExoPlanet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExoPlanet));
            this.DurationNumeric = new System.Windows.Forms.NumericUpDown();
            this.CandidateButton = new System.Windows.Forms.RadioButton();
            this.ConfirmedButton = new System.Windows.Forms.RadioButton();
            this.AltitudeNumeric = new System.Windows.Forms.NumericUpDown();
            this.TargetClassBox = new System.Windows.Forms.GroupBox();
            this.Closebutton = new System.Windows.Forms.Button();
            this.DurationTBMax = new System.Windows.Forms.Label();
            this.DurationTBMin = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.AltitudeTBMax = new System.Windows.Forms.Label();
            this.AltitudeTBMin = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.ProspectGrid = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RiseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StarMag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DurationNumeric)).BeginInit();
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
            this.DurationNumeric.Location = new System.Drawing.Point(44, 180);
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
            // CandidateButton
            // 
            this.CandidateButton.AutoSize = true;
            this.CandidateButton.Location = new System.Drawing.Point(21, 19);
            this.CandidateButton.Name = "CandidateButton";
            this.CandidateButton.Size = new System.Drawing.Size(73, 17);
            this.CandidateButton.TabIndex = 2;
            this.CandidateButton.Text = "Candidate";
            this.CandidateButton.UseVisualStyleBackColor = true;
            this.CandidateButton.CheckedChanged += new System.EventHandler(this.SelectCandidateExoPlanetButton_CheckedChanged);
            // 
            // ConfirmedButton
            // 
            this.ConfirmedButton.AutoSize = true;
            this.ConfirmedButton.Location = new System.Drawing.Point(21, 43);
            this.ConfirmedButton.Name = "ConfirmedButton";
            this.ConfirmedButton.Size = new System.Drawing.Size(72, 17);
            this.ConfirmedButton.TabIndex = 0;
            this.ConfirmedButton.Text = "Confirmed";
            this.ConfirmedButton.UseVisualStyleBackColor = true;
            this.ConfirmedButton.CheckedChanged += new System.EventHandler(this.SelectConfirmedExoPlanetButton_CheckedChanged);
            // 
            // AltitudeNumeric
            // 
            this.AltitudeNumeric.Location = new System.Drawing.Point(43, 128);
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
            this.TargetClassBox.Controls.Add(this.CandidateButton);
            this.TargetClassBox.Controls.Add(this.ConfirmedButton);
            this.TargetClassBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TargetClassBox.Location = new System.Drawing.Point(11, 14);
            this.TargetClassBox.Name = "TargetClassBox";
            this.TargetClassBox.Size = new System.Drawing.Size(112, 72);
            this.TargetClassBox.TabIndex = 87;
            this.TargetClassBox.TabStop = false;
            this.TargetClassBox.Text = "Target Class";
            // 
            // Closebutton
            // 
            this.Closebutton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Closebutton.Location = new System.Drawing.Point(44, 243);
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
            this.DurationTBMax.Location = new System.Drawing.Point(96, 182);
            this.DurationTBMax.Name = "DurationTBMax";
            this.DurationTBMax.Size = new System.Drawing.Size(27, 13);
            this.DurationTBMax.TabIndex = 83;
            this.DurationTBMax.Text = "Max";
            // 
            // DurationTBMin
            // 
            this.DurationTBMin.AutoSize = true;
            this.DurationTBMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DurationTBMin.Location = new System.Drawing.Point(13, 182);
            this.DurationTBMin.Name = "DurationTBMin";
            this.DurationTBMin.Size = new System.Drawing.Size(24, 13);
            this.DurationTBMin.TabIndex = 82;
            this.DurationTBMin.Text = "Min";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label10.Location = new System.Drawing.Point(21, 151);
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
            this.AltitudeTBMax.Location = new System.Drawing.Point(95, 130);
            this.AltitudeTBMax.Name = "AltitudeTBMax";
            this.AltitudeTBMax.Size = new System.Drawing.Size(27, 13);
            this.AltitudeTBMax.TabIndex = 80;
            this.AltitudeTBMax.Text = "Max";
            // 
            // AltitudeTBMin
            // 
            this.AltitudeTBMin.AutoSize = true;
            this.AltitudeTBMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AltitudeTBMin.Location = new System.Drawing.Point(12, 130);
            this.AltitudeTBMin.Name = "AltitudeTBMin";
            this.AltitudeTBMin.Size = new System.Drawing.Size(24, 13);
            this.AltitudeTBMin.TabIndex = 79;
            this.AltitudeTBMin.Text = "Min";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label2.Location = new System.Drawing.Point(21, 99);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(86, 26);
            this.Label2.TabIndex = 78;
            this.Label2.Text = "Minimum Altitude\r\n(deg)";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProspectGrid
            // 
            this.ProspectGrid.AllowUserToAddRows = false;
            this.ProspectGrid.AllowUserToDeleteRows = false;
            this.ProspectGrid.AllowUserToResizeColumns = false;
            this.ProspectGrid.AllowUserToResizeRows = false;
            this.ProspectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ProspectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.RiseColumn,
            this.SetColumn,
            this.Duration,
            this.DepthColumn,
            this.StarMag});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProspectGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.ProspectGrid.Location = new System.Drawing.Point(129, 14);
            this.ProspectGrid.MultiSelect = false;
            this.ProspectGrid.Name = "ProspectGrid";
            this.ProspectGrid.ReadOnly = true;
            this.ProspectGrid.RowHeadersVisible = false;
            this.ProspectGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ProspectGrid.Size = new System.Drawing.Size(462, 258);
            this.ProspectGrid.TabIndex = 91;
            this.ProspectGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProspectGrid_CellDoubleClickEvent);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.NullValue = null;
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.NameColumn.HeaderText = "Object Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NameColumn.Width = 94;
            // 
            // RiseColumn
            // 
            this.RiseColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.RiseColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.RiseColumn.HeaderText = "Start";
            this.RiseColumn.Name = "RiseColumn";
            this.RiseColumn.ReadOnly = true;
            this.RiseColumn.Width = 54;
            // 
            // SetColumn
            // 
            this.SetColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Format = "N1";
            dataGridViewCellStyle3.NullValue = null;
            this.SetColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.SetColumn.HeaderText = "End";
            this.SetColumn.Name = "SetColumn";
            this.SetColumn.ReadOnly = true;
            this.SetColumn.Width = 51;
            // 
            // Duration
            // 
            this.Duration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Duration.HeaderText = "Duration";
            this.Duration.Name = "Duration";
            this.Duration.ReadOnly = true;
            this.Duration.Width = 72;
            // 
            // DepthColumn
            // 
            this.DepthColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.DepthColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.DepthColumn.HeaderText = "Depth (Mag)";
            this.DepthColumn.Name = "DepthColumn";
            this.DepthColumn.ReadOnly = true;
            this.DepthColumn.Width = 91;
            // 
            // StarMag
            // 
            this.StarMag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StarMag.HeaderText = "Star Mag";
            this.StarMag.Name = "StarMag";
            this.StarMag.ReadOnly = true;
            this.StarMag.Width = 75;
            // 
            // FormExoPlanet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(603, 282);
            this.Controls.Add(this.ProspectGrid);
            this.Controls.Add(this.DurationNumeric);
            this.Controls.Add(this.AltitudeNumeric);
            this.Controls.Add(this.TargetClassBox);
            this.Controls.Add(this.Closebutton);
            this.Controls.Add(this.DurationTBMax);
            this.Controls.Add(this.DurationTBMin);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.AltitudeTBMax);
            this.Controls.Add(this.AltitudeTBMin);
            this.Controls.Add(this.Label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExoPlanet";
            this.ShowIcon = false;
            this.Text = "ExoPlanets";
            ((System.ComponentModel.ISupportInitialize)(this.DurationNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AltitudeNumeric)).EndInit();
            this.TargetClassBox.ResumeLayout(false);
            this.TargetClassBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProspectGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown DurationNumeric;
        private System.Windows.Forms.RadioButton CandidateButton;
        private System.Windows.Forms.RadioButton ConfirmedButton;
        private System.Windows.Forms.NumericUpDown AltitudeNumeric;
        private System.Windows.Forms.GroupBox TargetClassBox;
        internal System.Windows.Forms.Button Closebutton;
        internal System.Windows.Forms.Label DurationTBMax;
        internal System.Windows.Forms.Label DurationTBMin;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label AltitudeTBMax;
        internal System.Windows.Forms.Label AltitudeTBMin;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.DataGridView ProspectGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RiseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StarMag;
    }
}