﻿
namespace TestProgram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnSelectTest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFolderSelected = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblTestSelected = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStopTest = new System.Windows.Forms.Button();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txbLog = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblResourceStatus = new System.Windows.Forms.Label();
            this.pnlResourceStarted = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.chbLogEnabled = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lbxFilteredCanId = new System.Windows.Forms.ListBox();
            this.btnRemoveFilter = new System.Windows.Forms.Button();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.nudFilter = new System.Windows.Forms.NumericUpDown();
            this.btnReadLog = new System.Windows.Forms.Button();
            this.nudLogSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectTest
            // 
            this.btnSelectTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectTest.Location = new System.Drawing.Point(6, 19);
            this.btnSelectTest.Name = "btnSelectTest";
            this.btnSelectTest.Size = new System.Drawing.Size(188, 23);
            this.btnSelectTest.TabIndex = 0;
            this.btnSelectTest.Text = "Select test folder";
            this.btnSelectTest.UseVisualStyleBackColor = true;
            this.btnSelectTest.Click += new System.EventHandler(this.BtnSelectTest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFolderSelected);
            this.groupBox1.Controls.Add(this.btnSelectFolder);
            this.groupBox1.Controls.Add(this.lblTestSelected);
            this.groupBox1.Controls.Add(this.btnSelectTest);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 105);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File options";
            // 
            // lblFolderSelected
            // 
            this.lblFolderSelected.AutoSize = true;
            this.lblFolderSelected.Location = new System.Drawing.Point(6, 87);
            this.lblFolderSelected.Name = "lblFolderSelected";
            this.lblFolderSelected.Size = new System.Drawing.Size(97, 13);
            this.lblFolderSelected.TabIndex = 3;
            this.lblFolderSelected.Text = "Folder selected: ----";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFolder.Location = new System.Drawing.Point(6, 61);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(188, 23);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Select report folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.BtnSelectFolder_Click);
            // 
            // lblTestSelected
            // 
            this.lblTestSelected.AutoSize = true;
            this.lblTestSelected.Location = new System.Drawing.Point(6, 45);
            this.lblTestSelected.Name = "lblTestSelected";
            this.lblTestSelected.Size = new System.Drawing.Size(89, 13);
            this.lblTestSelected.TabIndex = 1;
            this.lblTestSelected.Text = "Test selected: ----";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStopTest);
            this.groupBox2.Controls.Add(this.btnStartTest);
            this.groupBox2.Location = new System.Drawing.Point(12, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 80);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test options";
            // 
            // btnStopTest
            // 
            this.btnStopTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopTest.Location = new System.Drawing.Point(6, 48);
            this.btnStopTest.Name = "btnStopTest";
            this.btnStopTest.Size = new System.Drawing.Size(188, 23);
            this.btnStopTest.TabIndex = 2;
            this.btnStopTest.Text = "Stop test";
            this.btnStopTest.UseVisualStyleBackColor = true;
            this.btnStopTest.Click += new System.EventHandler(this.BtnStopTest_Click);
            // 
            // btnStartTest
            // 
            this.btnStartTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTest.Location = new System.Drawing.Point(6, 19);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(188, 23);
            this.btnStartTest.TabIndex = 0;
            this.btnStartTest.Text = "Start test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.BtnStartTest_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txbLog);
            this.groupBox7.Location = new System.Drawing.Point(483, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(342, 427);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Log";
            // 
            // txbLog
            // 
            this.txbLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.txbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLog.ForeColor = System.Drawing.SystemColors.Info;
            this.txbLog.Location = new System.Drawing.Point(6, 19);
            this.txbLog.Multiline = true;
            this.txbLog.Name = "txbLog";
            this.txbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbLog.Size = new System.Drawing.Size(330, 401);
            this.txbLog.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Location = new System.Drawing.Point(218, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 78);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CAN resource commands";
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(6, 48);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(190, 23);
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
            this.btnStart.Size = new System.Drawing.Size(190, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbxBaudRate);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cbxDeviceList);
            this.groupBox4.Location = new System.Drawing.Point(11, 208);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(201, 71);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CAN resource settings";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(99, 41);
            this.cbxBaudRate.Margin = new System.Windows.Forms.Padding(2);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(96, 21);
            this.cbxBaudRate.TabIndex = 1;
            this.cbxBaudRate.SelectedIndexChanged += new System.EventHandler(this.CbxBaudRate_SelectedIndexChanged);
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
            // cbxDeviceList
            // 
            this.cbxDeviceList.FormattingEnabled = true;
            this.cbxDeviceList.Location = new System.Drawing.Point(99, 17);
            this.cbxDeviceList.Margin = new System.Windows.Forms.Padding(2);
            this.cbxDeviceList.Name = "cbxDeviceList";
            this.cbxDeviceList.Size = new System.Drawing.Size(96, 21);
            this.cbxDeviceList.TabIndex = 0;
            this.cbxDeviceList.SelectedIndexChanged += new System.EventHandler(this.CbxDeviceList_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblResourceStatus);
            this.groupBox6.Controls.Add(this.pnlResourceStarted);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.chbLogEnabled);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(218, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(259, 105);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CAN resource info";
            // 
            // lblResourceStatus
            // 
            this.lblResourceStatus.Location = new System.Drawing.Point(96, 50);
            this.lblResourceStatus.Name = "lblResourceStatus";
            this.lblResourceStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblResourceStatus.Size = new System.Drawing.Size(157, 19);
            this.lblResourceStatus.TabIndex = 3;
            this.lblResourceStatus.Text = "00000000";
            this.lblResourceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlResourceStarted
            // 
            this.pnlResourceStarted.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlResourceStarted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResourceStarted.Location = new System.Drawing.Point(237, 31);
            this.pnlResourceStarted.Name = "pnlResourceStarted";
            this.pnlResourceStarted.Size = new System.Drawing.Size(16, 16);
            this.pnlResourceStarted.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Resource status";
            // 
            // chbLogEnabled
            // 
            this.chbLogEnabled.AutoSize = true;
            this.chbLogEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbLogEnabled.Checked = true;
            this.chbLogEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbLogEnabled.Location = new System.Drawing.Point(1, 69);
            this.chbLogEnabled.Name = "chbLogEnabled";
            this.chbLogEnabled.Size = new System.Drawing.Size(85, 17);
            this.chbLogEnabled.TabIndex = 11;
            this.chbLogEnabled.Text = "Log enabled";
            this.chbLogEnabled.UseVisualStyleBackColor = true;
            this.chbLogEnabled.Click += new System.EventHandler(this.CbxLogEnabled_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Resource started";
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
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Location = new System.Drawing.Point(12, 284);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(413, 155);
            this.groupBox8.TabIndex = 20;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Log parameters";
            // 
            // lbxFilteredCanId
            // 
            this.lbxFilteredCanId.FormattingEnabled = true;
            this.lbxFilteredCanId.Location = new System.Drawing.Point(304, 14);
            this.lbxFilteredCanId.Name = "lbxFilteredCanId";
            this.lbxFilteredCanId.Size = new System.Drawing.Size(97, 134);
            this.lbxFilteredCanId.TabIndex = 1;
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFilter.Location = new System.Drawing.Point(12, 91);
            this.btnRemoveFilter.Name = "btnRemoveFilter";
            this.btnRemoveFilter.Size = new System.Drawing.Size(286, 23);
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
            this.btnAddFilter.Size = new System.Drawing.Size(286, 23);
            this.btnAddFilter.TabIndex = 16;
            this.btnAddFilter.Text = "Add CAN ID to filter";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.BtnAddFiltered_Click);
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
            this.nudFilter.Location = new System.Drawing.Point(211, 34);
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
            this.btnReadLog.Size = new System.Drawing.Size(286, 23);
            this.btnReadLog.TabIndex = 10;
            this.btnReadLog.Text = "Read log";
            this.btnReadLog.UseVisualStyleBackColor = true;
            this.btnReadLog.Click += new System.EventHandler(this.BtnReadLog_Click);
            // 
            // nudLogSize
            // 
            this.nudLogSize.Location = new System.Drawing.Point(211, 9);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Maximum log size";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 450);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Test program";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFolderSelected;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblTestSelected;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStopTest;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txbLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxDeviceList;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblResourceStatus;
        private System.Windows.Forms.Panel pnlResourceStarted;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chbLogEnabled;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListBox lbxFilteredCanId;
        private System.Windows.Forms.Button btnRemoveFilter;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudFilter;
        private System.Windows.Forms.Button btnReadLog;
        private System.Windows.Forms.NumericUpDown nudLogSize;
        private System.Windows.Forms.Label label9;
    }
}
