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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TargetDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.TargetDataGrid.Location = new System.Drawing.Point(0, 4);
            this.TargetDataGrid.MultiSelect = false;
            this.TargetDataGrid.Name = "TargetDataGrid";
            this.TargetDataGrid.ReadOnly = true;
            this.TargetDataGrid.RowHeadersVisible = false;
            this.TargetDataGrid.RowHeadersWidth = 123;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Blue;
            this.TargetDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.TargetDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TargetDataGrid.ShowEditingIcon = false;
            this.TargetDataGrid.Size = new System.Drawing.Size(615, 690);
            this.TargetDataGrid.TabIndex = 0;
            this.TargetDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TargetDataGrid_CellContentClick);
            // 
            // TargetName
            // 
            this.TargetName.HeaderText = "Target";
            this.TargetName.MinimumWidth = 15;
            this.TargetName.Name = "TargetName";
            this.TargetName.ReadOnly = true;
            this.TargetName.Width = 63;
            // 
            // Name2
            // 
            this.Name2.HeaderText = "Name2";
            this.Name2.MinimumWidth = 15;
            this.Name2.Name = "Name2";
            this.Name2.ReadOnly = true;
            this.Name2.Width = 66;
            // 
            // Species
            // 
            this.Species.HeaderText = "Species";
            this.Species.MinimumWidth = 15;
            this.Species.Name = "Species";
            this.Species.ReadOnly = true;
            this.Species.Width = 70;
            // 
            // MajorAxis
            // 
            this.MajorAxis.HeaderText = "Major Axis";
            this.MajorAxis.MinimumWidth = 15;
            this.MajorAxis.Name = "MajorAxis";
            this.MajorAxis.ReadOnly = true;
            this.MajorAxis.Width = 80;
            // 
            // MinorAxis
            // 
            this.MinorAxis.HeaderText = "MinorAxis";
            this.MinorAxis.MinimumWidth = 15;
            this.MinorAxis.Name = "MinorAxis";
            this.MinorAxis.ReadOnly = true;
            this.MinorAxis.Width = 77;
            // 
            // RiseTime
            // 
            this.RiseTime.HeaderText = "Rise";
            this.RiseTime.MinimumWidth = 15;
            this.RiseTime.Name = "RiseTime";
            this.RiseTime.ReadOnly = true;
            this.RiseTime.Width = 53;
            // 
            // TransitTime
            // 
            this.TransitTime.HeaderText = "Transit";
            this.TransitTime.MinimumWidth = 15;
            this.TransitTime.Name = "TransitTime";
            this.TransitTime.ReadOnly = true;
            this.TransitTime.Width = 64;
            // 
            // SetTime
            // 
            this.SetTime.HeaderText = "Set";
            this.SetTime.MinimumWidth = 15;
            this.SetTime.Name = "SetTime";
            this.SetTime.ReadOnly = true;
            this.SetTime.Width = 48;
            // 
            // Altitude
            // 
            this.Altitude.HeaderText = "Alt (now)";
            this.Altitude.MinimumWidth = 15;
            this.Altitude.Name = "Altitude";
            this.Altitude.ReadOnly = true;
            this.Altitude.Width = 73;
            // 
            // FormTargetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(633, 691);
            this.Controls.Add(this.TargetDataGrid);
            this.Name = "FormTargetList";
            this.Text = "Current Target Listing";
            ((System.ComponentModel.ISupportInitialize)(this.TargetDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TargetDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Species;
        private System.Windows.Forms.DataGridViewTextBoxColumn MajorAxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinorAxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn RiseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransitTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Altitude;
    }
}