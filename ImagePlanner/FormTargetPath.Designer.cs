namespace ImagePlanner
{
    partial class FormTargetPath
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTargetPath));
            this.MoonDataTextBox = new System.Windows.Forms.TextBox();
            this.AltitudeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.AltitudeChart)).BeginInit();
            this.SuspendLayout();
            // 
            // MoonDataTextBox
            // 
            this.MoonDataTextBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.MoonDataTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MoonDataTextBox.Location = new System.Drawing.Point(12, 188);
            this.MoonDataTextBox.Multiline = true;
            this.MoonDataTextBox.Name = "MoonDataTextBox";
            this.MoonDataTextBox.Size = new System.Drawing.Size(361, 38);
            this.MoonDataTextBox.TabIndex = 2;
            // 
            // AltitudeChart
            // 
            this.AltitudeChart.BackColor = System.Drawing.Color.MidnightBlue;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm";
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.Navy;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.AltitudeChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.AltitudeChart.Legends.Add(legend1);
            this.AltitudeChart.Location = new System.Drawing.Point(12, 12);
            this.AltitudeChart.Name = "AltitudeChart";
            this.AltitudeChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Color = System.Drawing.Color.OrangeRed;
            series1.LabelFormat = "\"HH:mm\"";
            series1.Legend = "Legend1";
            series1.Name = "AltitudePath";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Color = System.Drawing.Color.Yellow;
            series2.Legend = "Legend1";
            series2.Name = "MoonPath";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.AltitudeChart.Series.Add(series1);
            this.AltitudeChart.Series.Add(series2);
            this.AltitudeChart.Size = new System.Drawing.Size(361, 170);
            this.AltitudeChart.TabIndex = 3;
            this.AltitudeChart.Text = "chart1";
            // 
            // FormTargetPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 230);
            this.Controls.Add(this.AltitudeChart);
            this.Controls.Add(this.MoonDataTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTargetPath";
            this.ShowIcon = false;
            this.Text = "FormTargetPath";
            ((System.ComponentModel.ISupportInitialize)(this.AltitudeChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox MoonDataTextBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart AltitudeChart;
    }
}