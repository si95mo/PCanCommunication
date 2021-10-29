
namespace PCanCommunication
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cbxDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSetVariables = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbRSet = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.crtVariables = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblResourceStatus = new System.Windows.Forms.Label();
            this.pnlResourceStarted = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.chbLogEnabled = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lbxFilteredCanId = new System.Windows.Forms.ListBox();
            this.btnRemoveFilter = new System.Windows.Forms.Button();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.nudFilter = new System.Windows.Forms.NumericUpDown();
            this.btnReadLog = new System.Windows.Forms.Button();
            this.nudLogSize = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txbLog = new System.Windows.Forms.TextBox();
            this.cmsClearLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSetCanId = new System.Windows.Forms.Button();
            this.nudReceive = new System.Windows.Forms.NumericUpDown();
            this.nudSend = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblActualValue = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSetValue = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtVariables)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSize)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.cmsClearLog.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSend)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxDeviceList
            // 
            this.cbxDeviceList.FormattingEnabled = true;
            this.cbxDeviceList.Location = new System.Drawing.Point(99, 17);
            this.cbxDeviceList.Margin = new System.Windows.Forms.Padding(2);
            this.cbxDeviceList.Name = "cbxDeviceList";
            this.cbxDeviceList.Size = new System.Drawing.Size(188, 21);
            this.cbxDeviceList.TabIndex = 0;
            this.cbxDeviceList.SelectedIndexChanged += new System.EventHandler(this.CbxDeviceList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxDeviceList);
            this.groupBox1.Location = new System.Drawing.Point(9, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(296, 71);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAN settings";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(99, 41);
            this.cbxBaudRate.Margin = new System.Windows.Forms.Padding(2);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(188, 21);
            this.cbxBaudRate.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Baud rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Device list";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Location = new System.Drawing.Point(9, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 78);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CAN commands";
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(6, 48);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(281, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Location = new System.Drawing.Point(6, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(281, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSetVariables);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txbRSet);
            this.groupBox3.Location = new System.Drawing.Point(613, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 70);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CAN variables";
            // 
            // btnSetVariables
            // 
            this.btnSetVariables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetVariables.Location = new System.Drawing.Point(9, 40);
            this.btnSetVariables.Name = "btnSetVariables";
            this.btnSetVariables.Size = new System.Drawing.Size(281, 23);
            this.btnSetVariables.TabIndex = 5;
            this.btnSetVariables.Text = "Set";
            this.btnSetVariables.UseVisualStyleBackColor = true;
            this.btnSetVariables.Click += new System.EventHandler(this.BtnSetVariables_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Rset value";
            // 
            // txbRSet
            // 
            this.txbRSet.Location = new System.Drawing.Point(92, 16);
            this.txbRSet.Name = "txbRSet";
            this.txbRSet.Size = new System.Drawing.Size(198, 20);
            this.txbRSet.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.crtVariables);
            this.groupBox4.Location = new System.Drawing.Point(9, 168);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1202, 561);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CAN variables plot";
            // 
            // crtVariables
            // 
            this.crtVariables.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.LineColor = System.Drawing.Color.OrangeRed;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.IndianRed;
            chartArea1.AxisX2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LineColor = System.Drawing.Color.OrangeRed;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Firebrick;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.OrangeRed;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.RoyalBlue;
            chartArea1.AxisY2.LineWidth = 2;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.SlateBlue;
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.RoyalBlue;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea";
            this.crtVariables.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top;
            legend1.Enabled = false;
            legend1.MaximumAutoSize = 20F;
            legend1.Name = "Legend";
            this.crtVariables.Legends.Add(legend1);
            this.crtVariables.Location = new System.Drawing.Point(6, 19);
            this.crtVariables.Name = "crtVariables";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend";
            series1.Name = "ActSeries";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend";
            series2.Name = "SetSeries";
            this.crtVariables.Series.Add(series1);
            this.crtVariables.Series.Add(series2);
            this.crtVariables.Size = new System.Drawing.Size(1196, 536);
            this.crtVariables.TabIndex = 6;
            this.crtVariables.Text = "CAN variables";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblResourceStatus);
            this.groupBox5.Controls.Add(this.pnlResourceStarted);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.chbLogEnabled);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(915, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(296, 150);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CAN resource info";
            // 
            // lblResourceStatus
            // 
            this.lblResourceStatus.Location = new System.Drawing.Point(96, 34);
            this.lblResourceStatus.Name = "lblResourceStatus";
            this.lblResourceStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblResourceStatus.Size = new System.Drawing.Size(194, 19);
            this.lblResourceStatus.TabIndex = 3;
            this.lblResourceStatus.Text = "00000000";
            this.lblResourceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlResourceStarted
            // 
            this.pnlResourceStarted.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlResourceStarted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResourceStarted.Location = new System.Drawing.Point(268, 16);
            this.pnlResourceStarted.Name = "pnlResourceStarted";
            this.pnlResourceStarted.Size = new System.Drawing.Size(16, 16);
            this.pnlResourceStarted.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Resource status";
            // 
            // chbLogEnabled
            // 
            this.chbLogEnabled.AutoSize = true;
            this.chbLogEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbLogEnabled.Checked = true;
            this.chbLogEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbLogEnabled.Location = new System.Drawing.Point(6, 57);
            this.chbLogEnabled.Name = "chbLogEnabled";
            this.chbLogEnabled.Size = new System.Drawing.Size(85, 17);
            this.chbLogEnabled.TabIndex = 11;
            this.chbLogEnabled.Text = "Log enabled";
            this.chbLogEnabled.UseVisualStyleBackColor = true;
            this.chbLogEnabled.CheckedChanged += new System.EventHandler(this.CbxLogEnabled_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Resource started";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lbxFilteredCanId);
            this.groupBox8.Controls.Add(this.btnRemoveFilter);
            this.groupBox8.Controls.Add(this.btnAddFilter);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.nudFilter);
            this.groupBox8.Controls.Add(this.btnReadLog);
            this.groupBox8.Controls.Add(this.nudLogSize);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Location = new System.Drawing.Point(1217, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(344, 150);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Log parameters";
            // 
            // lbxFilteredCanId
            // 
            this.lbxFilteredCanId.FormattingEnabled = true;
            this.lbxFilteredCanId.Location = new System.Drawing.Point(250, 62);
            this.lbxFilteredCanId.Name = "lbxFilteredCanId";
            this.lbxFilteredCanId.Size = new System.Drawing.Size(87, 82);
            this.lbxFilteredCanId.TabIndex = 1;
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFilter.Location = new System.Drawing.Point(12, 91);
            this.btnRemoveFilter.Name = "btnRemoveFilter";
            this.btnRemoveFilter.Size = new System.Drawing.Size(232, 23);
            this.btnRemoveFilter.TabIndex = 17;
            this.btnRemoveFilter.Text = "Remove CAN ID to filter";
            this.btnRemoveFilter.UseVisualStyleBackColor = true;
            this.btnRemoveFilter.Click += new System.EventHandler(this.BtnRemoveFilter_Click);
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFilter.Location = new System.Drawing.Point(12, 62);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(232, 23);
            this.btnAddFilter.TabIndex = 16;
            this.btnAddFilter.Text = "Add CAN ID to filter";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.BtnAddFiltered);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Filtered CAN id (hexadecimal):";
            // 
            // nudFilter
            // 
            this.nudFilter.Hexadecimal = true;
            this.nudFilter.Location = new System.Drawing.Point(250, 34);
            this.nudFilter.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudFilter.Name = "nudFilter";
            this.nudFilter.Size = new System.Drawing.Size(87, 20);
            this.nudFilter.TabIndex = 14;
            this.nudFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudFilter.Value = new decimal(new int[] {
            384,
            0,
            0,
            0});
            // 
            // btnReadLog
            // 
            this.btnReadLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadLog.Location = new System.Drawing.Point(12, 120);
            this.btnReadLog.Name = "btnReadLog";
            this.btnReadLog.Size = new System.Drawing.Size(232, 23);
            this.btnReadLog.TabIndex = 10;
            this.btnReadLog.Text = "Read log";
            this.btnReadLog.UseVisualStyleBackColor = true;
            this.btnReadLog.Click += new System.EventHandler(this.BtnReadLog_Click);
            // 
            // nudLogSize
            // 
            this.nudLogSize.Location = new System.Drawing.Point(250, 14);
            this.nudLogSize.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudLogSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLogSize.Name = "nudLogSize";
            this.nudLogSize.Size = new System.Drawing.Size(87, 20);
            this.nudLogSize.TabIndex = 12;
            this.nudLogSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLogSize.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Maximum log size";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txbLog);
            this.groupBox7.Location = new System.Drawing.Point(1217, 168);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(344, 561);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Log";
            // 
            // txbLog
            // 
            this.txbLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.txbLog.ContextMenuStrip = this.cmsClearLog;
            this.txbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLog.ForeColor = System.Drawing.SystemColors.Info;
            this.txbLog.Location = new System.Drawing.Point(6, 19);
            this.txbLog.Multiline = true;
            this.txbLog.Name = "txbLog";
            this.txbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbLog.Size = new System.Drawing.Size(331, 536);
            this.txbLog.TabIndex = 0;
            this.txbLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxbLog_MouseDown);
            // 
            // cmsClearLog
            // 
            this.cmsClearLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItem});
            this.cmsClearLog.Name = "cmsClearLog";
            this.cmsClearLog.Size = new System.Drawing.Size(102, 26);
            this.cmsClearLog.Text = "Clear log";
            this.cmsClearLog.Click += new System.EventHandler(this.CmsClearLog_Click);
            // 
            // tsmItem
            // 
            this.tsmItem.Name = "tsmItem";
            this.tsmItem.Size = new System.Drawing.Size(101, 22);
            this.tsmItem.Text = "Clear";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSetCanId);
            this.groupBox9.Controls.Add(this.nudReceive);
            this.groupBox9.Controls.Add(this.nudSend);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Controls.Add(this.label13);
            this.groupBox9.Location = new System.Drawing.Point(311, 12);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(296, 150);
            this.groupBox9.TabIndex = 14;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "CAN IDs";
            // 
            // btnSetCanId
            // 
            this.btnSetCanId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetCanId.Location = new System.Drawing.Point(9, 120);
            this.btnSetCanId.Name = "btnSetCanId";
            this.btnSetCanId.Size = new System.Drawing.Size(281, 23);
            this.btnSetCanId.TabIndex = 12;
            this.btnSetCanId.Text = "Set";
            this.btnSetCanId.UseVisualStyleBackColor = true;
            this.btnSetCanId.Click += new System.EventHandler(this.BtnSetCanId_Click);
            // 
            // nudReceive
            // 
            this.nudReceive.Hexadecimal = true;
            this.nudReceive.Location = new System.Drawing.Point(202, 37);
            this.nudReceive.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.nudReceive.Name = "nudReceive";
            this.nudReceive.Size = new System.Drawing.Size(88, 20);
            this.nudReceive.TabIndex = 11;
            this.nudReceive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudSend
            // 
            this.nudSend.Hexadecimal = true;
            this.nudSend.Location = new System.Drawing.Point(202, 14);
            this.nudSend.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudSend.Name = "nudSend";
            this.nudSend.Size = new System.Drawing.Size(88, 20);
            this.nudSend.TabIndex = 10;
            this.nudSend.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Receive id: 0x180 + (hexadecimal)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Send id: 0x200 + (hexadecimal)";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.lblActualValue);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.lblSetValue);
            this.groupBox6.Location = new System.Drawing.Point(613, 84);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(296, 78);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Actual values";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Ract: ";
            // 
            // lblActualValue
            // 
            this.lblActualValue.AutoSize = true;
            this.lblActualValue.Location = new System.Drawing.Point(61, 30);
            this.lblActualValue.Name = "lblActualValue";
            this.lblActualValue.Size = new System.Drawing.Size(31, 13);
            this.lblActualValue.TabIndex = 15;
            this.lblActualValue.Text = "........";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Rset: ";
            // 
            // lblSetValue
            // 
            this.lblSetValue.AutoSize = true;
            this.lblSetValue.Location = new System.Drawing.Point(61, 43);
            this.lblSetValue.Name = "lblSetValue";
            this.lblSetValue.Size = new System.Drawing.Size(31, 13);
            this.lblSetValue.TabIndex = 16;
            this.lblSetValue.Text = "........";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1574, 741);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCan communication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crtVariables)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSize)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.cmsClearLog.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSend)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxDeviceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSetVariables;
        private System.Windows.Forms.TextBox txbRSet;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtVariables;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel pnlResourceStarted;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblResourceStatus;
        private System.Windows.Forms.Button btnReadLog;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chbLogEnabled;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.NumericUpDown nudLogSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnSetCanId;
        private System.Windows.Forms.NumericUpDown nudReceive;
        private System.Windows.Forms.NumericUpDown nudSend;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudFilter;
        private System.Windows.Forms.Button btnRemoveFilter;
        private System.Windows.Forms.ListBox lbxFilteredCanId;
        private System.Windows.Forms.ContextMenuStrip cmsClearLog;
        private System.Windows.Forms.ToolStripMenuItem tsmItem;
        private System.Windows.Forms.TextBox txbLog;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblActualValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSetValue;
    }
}

