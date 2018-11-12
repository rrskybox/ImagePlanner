namespace ImagePlanner
{
    partial class FormImagePlanner
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImagePlanner));
            this.InfoButton = new System.Windows.Forms.Button();
            this.AltitudeButton = new System.Windows.Forms.Button();
            this.TargetDetailsTip = new System.Windows.Forms.ToolTip(this.components);
            this.DetailsButton = new System.Windows.Forms.Button();
            this.DeleteTargetPlanButton = new System.Windows.Forms.Button();
            this.AddTargetPlanButton = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.ImagePlannerTargetList = new System.Windows.Forms.ComboBox();
            this.TargetNameDownButton = new System.Windows.Forms.Button();
            this.TargetNameUpButton = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.MinAltitudeBox = new System.Windows.Forms.NumericUpDown();
            this.DoneButton = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.MonthCalendar = new System.Windows.Forms.DataGridView();
            this.PreviewButton = new System.Windows.Forms.Button();
            this.TargetNameBox = new System.Windows.Forms.TextBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CurrentYearPick = new System.Windows.Forms.NumericUpDown();
            this.printCalendar = new System.Drawing.Printing.PrintDocument();
            this.ProspectButton = new System.Windows.Forms.Button();
            this.TrackButton = new System.Windows.Forms.Button();
            this.January = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Februrary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.March = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.April = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.May = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.June = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.July = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.August = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.September = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.October = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.November = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.December = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MinAltitudeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthCalendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentYearPick)).BeginInit();
            this.SuspendLayout();
            // 
            // InfoButton
            // 
            this.InfoButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.InfoButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.InfoButton.Location = new System.Drawing.Point(1360, 9);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(46, 20);
            this.InfoButton.TabIndex = 59;
            this.InfoButton.Text = "Info";
            this.InfoButton.UseVisualStyleBackColor = false;
            this.InfoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // AltitudeButton
            // 
            this.AltitudeButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.AltitudeButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AltitudeButton.Location = new System.Drawing.Point(420, 9);
            this.AltitudeButton.Name = "AltitudeButton";
            this.AltitudeButton.Size = new System.Drawing.Size(60, 20);
            this.AltitudeButton.TabIndex = 57;
            this.AltitudeButton.Text = "Altitude";
            this.AltitudeButton.UseVisualStyleBackColor = false;
            this.AltitudeButton.Click += new System.EventHandler(this.AltitudeButton_Click);
            // 
            // TargetDetailsTip
            // 
            this.TargetDetailsTip.AutomaticDelay = 100;
            this.TargetDetailsTip.AutoPopDelay = 9000;
            this.TargetDetailsTip.InitialDelay = 100;
            this.TargetDetailsTip.IsBalloon = true;
            this.TargetDetailsTip.ReshowDelay = 20;
            this.TargetDetailsTip.ShowAlways = true;
            // 
            // DetailsButton
            // 
            this.DetailsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DetailsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DetailsButton.Location = new System.Drawing.Point(550, 9);
            this.DetailsButton.Name = "DetailsButton";
            this.DetailsButton.Size = new System.Drawing.Size(60, 20);
            this.DetailsButton.TabIndex = 56;
            this.DetailsButton.Text = "Details";
            this.DetailsButton.UseVisualStyleBackColor = false;
            this.DetailsButton.Click += new System.EventHandler(this.DetailsButton_Click);
            // 
            // DeleteTargetPlanButton
            // 
            this.DeleteTargetPlanButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DeleteTargetPlanButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DeleteTargetPlanButton.Location = new System.Drawing.Point(1209, 10);
            this.DeleteTargetPlanButton.Name = "DeleteTargetPlanButton";
            this.DeleteTargetPlanButton.Size = new System.Drawing.Size(60, 20);
            this.DeleteTargetPlanButton.TabIndex = 55;
            this.DeleteTargetPlanButton.Text = "Remove";
            this.DeleteTargetPlanButton.UseVisualStyleBackColor = false;
            this.DeleteTargetPlanButton.Click += new System.EventHandler(this.DeleteTargetPlanButton_Click);
            // 
            // AddTargetPlanButton
            // 
            this.AddTargetPlanButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.AddTargetPlanButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddTargetPlanButton.Location = new System.Drawing.Point(1143, 10);
            this.AddTargetPlanButton.Name = "AddTargetPlanButton";
            this.AddTargetPlanButton.Size = new System.Drawing.Size(60, 20);
            this.AddTargetPlanButton.TabIndex = 54;
            this.AddTargetPlanButton.Text = "Add";
            this.AddTargetPlanButton.UseVisualStyleBackColor = false;
            this.AddTargetPlanButton.Click += new System.EventHandler(this.AddTargetPlanButton_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label4.Location = new System.Drawing.Point(916, 15);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(94, 13);
            this.Label4.TabIndex = 53;
            this.Label4.Text = "Current Target List";
            // 
            // ImagePlannerTargetList
            // 
            this.ImagePlannerTargetList.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ImagePlannerTargetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ImagePlannerTargetList.FormattingEnabled = true;
            this.ImagePlannerTargetList.Location = new System.Drawing.Point(1016, 11);
            this.ImagePlannerTargetList.MaxDropDownItems = 20;
            this.ImagePlannerTargetList.Name = "ImagePlannerTargetList";
            this.ImagePlannerTargetList.Size = new System.Drawing.Size(121, 21);
            this.ImagePlannerTargetList.TabIndex = 52;
            this.ImagePlannerTargetList.SelectedIndexChanged += new System.EventHandler(this.ImagePlannerTargetList_SelectedIndexChanged);
            // 
            // TargetNameDownButton
            // 
            this.TargetNameDownButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TargetNameDownButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TargetNameDownButton.Location = new System.Drawing.Point(320, 9);
            this.TargetNameDownButton.Name = "TargetNameDownButton";
            this.TargetNameDownButton.Size = new System.Drawing.Size(27, 20);
            this.TargetNameDownButton.TabIndex = 51;
            this.TargetNameDownButton.Text = "\\/";
            this.TargetNameDownButton.UseVisualStyleBackColor = false;
            this.TargetNameDownButton.Click += new System.EventHandler(this.TargetNameDownButton_Click);
            // 
            // TargetNameUpButton
            // 
            this.TargetNameUpButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TargetNameUpButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TargetNameUpButton.Location = new System.Drawing.Point(287, 9);
            this.TargetNameUpButton.Name = "TargetNameUpButton";
            this.TargetNameUpButton.Size = new System.Drawing.Size(27, 20);
            this.TargetNameUpButton.TabIndex = 50;
            this.TargetNameUpButton.Text = "/\\";
            this.TargetNameUpButton.UseVisualStyleBackColor = false;
            this.TargetNameUpButton.Click += new System.EventHandler(this.TargetNameUpButton_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label3.Location = new System.Drawing.Point(748, 15);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(115, 13);
            this.Label3.TabIndex = 49;
            this.Label3.Text = "Minimum Altitude (Deg)";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label2.Location = new System.Drawing.Point(13, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(29, 13);
            this.Label2.TabIndex = 48;
            this.Label2.Text = "Year";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label1.Location = new System.Drawing.Point(140, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(38, 13);
            this.Label1.TabIndex = 47;
            this.Label1.Text = "Target";
            // 
            // MinAltitudeBox
            // 
            this.MinAltitudeBox.Location = new System.Drawing.Point(869, 11);
            this.MinAltitudeBox.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.MinAltitudeBox.Name = "MinAltitudeBox";
            this.MinAltitudeBox.Size = new System.Drawing.Size(41, 20);
            this.MinAltitudeBox.TabIndex = 46;
            this.MinAltitudeBox.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // DoneButton
            // 
            this.DoneButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DoneButton.Location = new System.Drawing.Point(1412, 9);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(46, 20);
            this.DoneButton.TabIndex = 45;
            this.DoneButton.Text = "Close";
            this.DoneButton.UseVisualStyleBackColor = false;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PrintButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PrintButton.Location = new System.Drawing.Point(1308, 9);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(46, 20);
            this.PrintButton.TabIndex = 44;
            this.PrintButton.Text = "Print";
            this.PrintButton.UseVisualStyleBackColor = false;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // MonthCalendar
            // 
            this.MonthCalendar.AllowUserToAddRows = false;
            this.MonthCalendar.AllowUserToDeleteRows = false;
            this.MonthCalendar.AllowUserToResizeColumns = false;
            this.MonthCalendar.AllowUserToResizeRows = false;
            this.MonthCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MonthCalendar.BackgroundColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MonthCalendar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MonthCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MonthCalendar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.January,
            this.Februrary,
            this.March,
            this.April,
            this.May,
            this.June,
            this.July,
            this.August,
            this.September,
            this.October,
            this.November,
            this.December});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MonthCalendar.DefaultCellStyle = dataGridViewCellStyle14;
            this.MonthCalendar.Location = new System.Drawing.Point(16, 45);
            this.MonthCalendar.MultiSelect = false;
            this.MonthCalendar.Name = "MonthCalendar";
            this.MonthCalendar.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.Format = "N0";
            dataGridViewCellStyle15.NullValue = null;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MonthCalendar.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.MonthCalendar.RowHeadersWidth = 60;
            this.MonthCalendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.MonthCalendar.Size = new System.Drawing.Size(1431, 724);
            this.MonthCalendar.TabIndex = 40;
            this.MonthCalendar.SelectionChanged += new System.EventHandler(this.MonthCalendar_SelectionChanged);
            // 
            // PreviewButton
            // 
            this.PreviewButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PreviewButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PreviewButton.Location = new System.Drawing.Point(616, 10);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(60, 20);
            this.PreviewButton.TabIndex = 58;
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // TargetNameBox
            // 
            this.TargetNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TargetNameBox.Location = new System.Drawing.Point(184, 9);
            this.TargetNameBox.Name = "TargetNameBox";
            this.TargetNameBox.Size = new System.Drawing.Size(97, 20);
            this.TargetNameBox.TabIndex = 42;
            this.TargetNameBox.Text = "M1";
            this.TargetNameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TargetNameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TargetNameBox_TextChanged);
            // 
            // CreateButton
            // 
            this.CreateButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CreateButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CreateButton.Location = new System.Drawing.Point(353, 9);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(61, 20);
            this.CreateButton.TabIndex = 41;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = false;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CurrentYearPick
            // 
            this.CurrentYearPick.Location = new System.Drawing.Point(57, 9);
            this.CurrentYearPick.Maximum = new decimal(new int[] {
            2200,
            0,
            0,
            0});
            this.CurrentYearPick.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.CurrentYearPick.Name = "CurrentYearPick";
            this.CurrentYearPick.Size = new System.Drawing.Size(67, 20);
            this.CurrentYearPick.TabIndex = 43;
            this.CurrentYearPick.Value = new decimal(new int[] {
            2018,
            0,
            0,
            0});
            // 
            // printCalendar
            // 
            this.printCalendar.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintCalendar_PrintPage);
            // 
            // ProspectButton
            // 
            this.ProspectButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ProspectButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ProspectButton.Location = new System.Drawing.Point(682, 10);
            this.ProspectButton.Name = "ProspectButton";
            this.ProspectButton.Size = new System.Drawing.Size(60, 20);
            this.ProspectButton.TabIndex = 61;
            this.ProspectButton.Text = "Prospect";
            this.ProspectButton.UseVisualStyleBackColor = false;
            this.ProspectButton.Click += new System.EventHandler(this.ProspectButton_Click);
            // 
            // TrackButton
            // 
            this.TrackButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TrackButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TrackButton.Location = new System.Drawing.Point(484, 9);
            this.TrackButton.Name = "TrackButton";
            this.TrackButton.Size = new System.Drawing.Size(60, 20);
            this.TrackButton.TabIndex = 62;
            this.TrackButton.Text = "Track";
            this.TrackButton.UseVisualStyleBackColor = false;
            this.TrackButton.Click += new System.EventHandler(this.TrackButton_Click);
            // 
            // January
            // 
            this.January.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.January.DefaultCellStyle = dataGridViewCellStyle2;
            this.January.HeaderText = "January";
            this.January.Name = "January";
            this.January.ReadOnly = true;
            this.January.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.January.Width = 5;
            // 
            // Februrary
            // 
            this.Februrary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Februrary.DefaultCellStyle = dataGridViewCellStyle3;
            this.Februrary.HeaderText = "February";
            this.Februrary.Name = "Februrary";
            this.Februrary.ReadOnly = true;
            this.Februrary.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Februrary.Width = 5;
            // 
            // March
            // 
            this.March.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.March.DefaultCellStyle = dataGridViewCellStyle4;
            this.March.HeaderText = "March";
            this.March.Name = "March";
            this.March.ReadOnly = true;
            this.March.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.March.Width = 5;
            // 
            // April
            // 
            this.April.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.April.DefaultCellStyle = dataGridViewCellStyle5;
            this.April.HeaderText = "April";
            this.April.Name = "April";
            this.April.ReadOnly = true;
            this.April.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.April.Width = 5;
            // 
            // May
            // 
            this.May.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.May.DefaultCellStyle = dataGridViewCellStyle6;
            this.May.HeaderText = "May";
            this.May.Name = "May";
            this.May.ReadOnly = true;
            this.May.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.May.Width = 5;
            // 
            // June
            // 
            this.June.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.June.DefaultCellStyle = dataGridViewCellStyle7;
            this.June.HeaderText = "June";
            this.June.Name = "June";
            this.June.ReadOnly = true;
            this.June.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.June.Width = 5;
            // 
            // July
            // 
            this.July.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.July.DefaultCellStyle = dataGridViewCellStyle8;
            this.July.HeaderText = "July";
            this.July.Name = "July";
            this.July.ReadOnly = true;
            this.July.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.July.Width = 5;
            // 
            // August
            // 
            this.August.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.August.DefaultCellStyle = dataGridViewCellStyle9;
            this.August.HeaderText = "August";
            this.August.Name = "August";
            this.August.ReadOnly = true;
            this.August.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.August.Width = 5;
            // 
            // September
            // 
            this.September.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.September.DefaultCellStyle = dataGridViewCellStyle10;
            this.September.HeaderText = "September";
            this.September.Name = "September";
            this.September.ReadOnly = true;
            this.September.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.September.Width = 5;
            // 
            // October
            // 
            this.October.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.October.DefaultCellStyle = dataGridViewCellStyle11;
            this.October.HeaderText = "Octorber";
            this.October.Name = "October";
            this.October.ReadOnly = true;
            this.October.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.October.Width = 5;
            // 
            // November
            // 
            this.November.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.November.DefaultCellStyle = dataGridViewCellStyle12;
            this.November.HeaderText = "November";
            this.November.Name = "November";
            this.November.ReadOnly = true;
            this.November.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.November.Width = 5;
            // 
            // December
            // 
            this.December.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.December.DefaultCellStyle = dataGridViewCellStyle13;
            this.December.HeaderText = "December";
            this.December.Name = "December";
            this.December.ReadOnly = true;
            this.December.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.December.Width = 5;
            // 
            // FormImagePlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1464, 781);
            this.Controls.Add(this.TrackButton);
            this.Controls.Add(this.ProspectButton);
            this.Controls.Add(this.InfoButton);
            this.Controls.Add(this.AltitudeButton);
            this.Controls.Add(this.DetailsButton);
            this.Controls.Add(this.DeleteTargetPlanButton);
            this.Controls.Add(this.AddTargetPlanButton);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ImagePlannerTargetList);
            this.Controls.Add(this.TargetNameDownButton);
            this.Controls.Add(this.TargetNameUpButton);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.MinAltitudeBox);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.MonthCalendar);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.TargetNameBox);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.CurrentYearPick);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormImagePlanner";
            this.Text = "Image Planner";
            ((System.ComponentModel.ISupportInitialize)(this.MinAltitudeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthCalendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentYearPick)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button InfoButton;
        internal System.Windows.Forms.Button AltitudeButton;
        internal System.Windows.Forms.ToolTip TargetDetailsTip;
        internal System.Windows.Forms.Button DetailsButton;
        internal System.Windows.Forms.Button DeleteTargetPlanButton;
        internal System.Windows.Forms.Button AddTargetPlanButton;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox ImagePlannerTargetList;
        internal System.Windows.Forms.Button TargetNameDownButton;
        internal System.Windows.Forms.Button TargetNameUpButton;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.NumericUpDown MinAltitudeBox;
        internal System.Windows.Forms.Button DoneButton;
        internal System.Windows.Forms.Button PrintButton;
        internal System.Windows.Forms.DataGridView MonthCalendar;
        internal System.Windows.Forms.Button PreviewButton;
        internal System.Windows.Forms.TextBox TargetNameBox;
        internal System.Windows.Forms.Button CreateButton;
        internal System.Windows.Forms.NumericUpDown CurrentYearPick;
        private System.Drawing.Printing.PrintDocument printCalendar;
        internal System.Windows.Forms.Button ProspectButton;
        internal System.Windows.Forms.Button TrackButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn January;
        private System.Windows.Forms.DataGridViewTextBoxColumn Februrary;
        private System.Windows.Forms.DataGridViewTextBoxColumn March;
        private System.Windows.Forms.DataGridViewTextBoxColumn April;
        private System.Windows.Forms.DataGridViewTextBoxColumn May;
        private System.Windows.Forms.DataGridViewTextBoxColumn June;
        private System.Windows.Forms.DataGridViewTextBoxColumn July;
        private System.Windows.Forms.DataGridViewTextBoxColumn August;
        private System.Windows.Forms.DataGridViewTextBoxColumn September;
        private System.Windows.Forms.DataGridViewTextBoxColumn October;
        private System.Windows.Forms.DataGridViewTextBoxColumn November;
        private System.Windows.Forms.DataGridViewTextBoxColumn December;
    }
}

