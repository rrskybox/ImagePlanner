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
            this.CandidateButton = new System.Windows.Forms.RadioButton();
            this.ConfirmedButton = new System.Windows.Forms.RadioButton();
            this.TargetClassBox = new System.Windows.Forms.GroupBox();
            this.Closebutton = new System.Windows.Forms.Button();
            this.ProspectGrid = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RiseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartAlt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndAlt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StarMag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinAlt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetClassBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProspectGrid)).BeginInit();
            this.SuspendLayout();
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
            this.StartAlt,
            this.SetColumn,
            this.EndAlt,
            this.Duration,
            this.DepthColumn,
            this.StarMag,
            this.MinAlt});
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
            this.ProspectGrid.Size = new System.Drawing.Size(598, 258);
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
            // StartAlt
            // 
            this.StartAlt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StartAlt.HeaderText = "@Alt";
            this.StartAlt.Name = "StartAlt";
            this.StartAlt.ReadOnly = true;
            this.StartAlt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StartAlt.Width = 55;
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
            // EndAlt
            // 
            this.EndAlt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EndAlt.HeaderText = "@Alt";
            this.EndAlt.Name = "EndAlt";
            this.EndAlt.ReadOnly = true;
            this.EndAlt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EndAlt.Width = 55;
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
            this.DepthColumn.HeaderText = "Depth";
            this.DepthColumn.Name = "DepthColumn";
            this.DepthColumn.ReadOnly = true;
            this.DepthColumn.Width = 61;
            // 
            // StarMag
            // 
            this.StarMag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StarMag.HeaderText = "Star Mag";
            this.StarMag.Name = "StarMag";
            this.StarMag.ReadOnly = true;
            this.StarMag.Width = 75;
            // 
            // MinAlt
            // 
            this.MinAlt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MinAlt.HeaderText = "Min Alt";
            this.MinAlt.Name = "MinAlt";
            this.MinAlt.ReadOnly = true;
            this.MinAlt.Width = 64;
            // 
            // FormExoPlanet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(736, 282);
            this.Controls.Add(this.ProspectGrid);
            this.Controls.Add(this.TargetClassBox);
            this.Controls.Add(this.Closebutton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExoPlanet";
            this.ShowIcon = false;
            this.Text = "ExoPlanets";
            this.TargetClassBox.ResumeLayout(false);
            this.TargetClassBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProspectGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton CandidateButton;
        private System.Windows.Forms.RadioButton ConfirmedButton;
        private System.Windows.Forms.GroupBox TargetClassBox;
        internal System.Windows.Forms.Button Closebutton;
        private System.Windows.Forms.DataGridView ProspectGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RiseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartAlt;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndAlt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StarMag;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinAlt;
    }
}