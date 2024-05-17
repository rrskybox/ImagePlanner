using System.Windows.Forms;

namespace ImagePlanner
{
    partial class FormTargetList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TargetDataGrid = new System.Windows.Forms.DataGridView();
            this.TargetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Species = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MajorAxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinorAxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RiseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransitTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Altitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TargetDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TargetDataGrid
            // 
            this.TargetDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TargetDataGrid.BackgroundColor = System.Drawing.Color.MidnightBlue;
            this.TargetDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.TargetDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TargetName,
            this.Name2,
            this.Species,
            this.MajorAxis,
            this.MinorAxis,
            this.RiseTime,
            this.TransitTime,
            this.SetTime,
            this.Altitude});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TargetDataGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.TargetDataGrid.Location = new System.Drawing.Point(2, 1);
            this.TargetDataGrid.MultiSelect = false;
            this.TargetDataGrid.Name = "TargetDataGrid";
            this.TargetDataGrid.ReadOnly = true;
            this.TargetDataGrid.RowHeadersVisible = false;
            this.TargetDataGrid.RowHeadersWidth = 123;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Blue;
            this.TargetDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.TargetDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TargetDataGrid.ShowEditingIcon = false;
            this.TargetDataGrid.Size = new System.Drawing.Size(881, 690);
            this.TargetDataGrid.TabIndex = 0;
            this.TargetDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TargetDataGrid_CellContentClick);
            // 
            // TargetName
            // 
            this.TargetName.HeaderText = "Target";
            this.TargetName.MinimumWidth = 150;
            this.TargetName.Name = "TargetName";
            this.TargetName.ReadOnly = true;
            this.TargetName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TargetName.Width = 150;
            // 
            // Name2
            // 
            this.Name2.HeaderText = "Name2";
            this.Name2.MinimumWidth = 150;
            this.Name2.Name = "Name2";
            this.Name2.ReadOnly = true;
            this.Name2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Name2.Width = 150;
            // 
            // Species
            // 
            this.Species.HeaderText = "Species";
            this.Species.MinimumWidth = 40;
            this.Species.Name = "Species";
            this.Species.ReadOnly = true;
            this.Species.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Species.Width = 70;
            // 
            // MajorAxis
            // 
            this.MajorAxis.HeaderText = "Major Axis";
            this.MajorAxis.MinimumWidth = 80;
            this.MajorAxis.Name = "MajorAxis";
            this.MajorAxis.ReadOnly = true;
            this.MajorAxis.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MajorAxis.Width = 80;
            // 
            // MinorAxis
            // 
            this.MinorAxis.HeaderText = "MinorAxis";
            this.MinorAxis.MinimumWidth = 80;
            this.MinorAxis.Name = "MinorAxis";
            this.MinorAxis.ReadOnly = true;
            this.MinorAxis.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MinorAxis.Width = 80;
            // 
            // RiseTime
            // 
            this.RiseTime.HeaderText = "Rise";
            this.RiseTime.MinimumWidth = 84;
            this.RiseTime.Name = "RiseTime";
            this.RiseTime.ReadOnly = true;
            this.RiseTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RiseTime.Width = 84;
            // 
            // TransitTime
            // 
            this.TransitTime.HeaderText = "Transit";
            this.TransitTime.MinimumWidth = 64;
            this.TransitTime.Name = "TransitTime";
            this.TransitTime.ReadOnly = true;
            this.TransitTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TransitTime.Width = 64;
            // 
            // SetTime
            // 
            this.SetTime.HeaderText = "Set";
            this.SetTime.MinimumWidth = 64;
            this.SetTime.Name = "SetTime";
            this.SetTime.ReadOnly = true;
            this.SetTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SetTime.Width = 64;
            // 
            // Altitude
            // 
            this.Altitude.HeaderText = "Alt (now)";
            this.Altitude.MinimumWidth = 75;
            this.Altitude.Name = "Altitude";
            this.Altitude.ReadOnly = true;
            this.Altitude.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Altitude.Width = 75;
            // 
            // FormTargetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(891, 700);
            this.Controls.Add(this.TargetDataGrid);
            this.Name = "FormTargetList";
            this.Text = "Current Target Listing";
            ((System.ComponentModel.ISupportInitialize)(this.TargetDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TargetDataGrid;
        private DataGridViewTextBoxColumn TargetName;
        private DataGridViewTextBoxColumn Name2;
        private DataGridViewTextBoxColumn Species;
        private DataGridViewTextBoxColumn MajorAxis;
        private DataGridViewTextBoxColumn MinorAxis;
        private DataGridViewTextBoxColumn RiseTime;
        private DataGridViewTextBoxColumn TransitTime;
        private DataGridViewTextBoxColumn SetTime;
        private DataGridViewTextBoxColumn Altitude;
    }
}