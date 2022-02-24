
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
            this.txbTestLog = new System.Windows.Forms.TextBox();
            this.btnStartResource = new System.Windows.Forms.Button();
            this.lblResourceStatus = new System.Windows.Forms.Label();
            this.pnlResourceStarted = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.chbLogEnabled = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lbxFilteredCanId = new System.Windows.Forms.ListBox();
            this.btnRemoveFilter = new System.Windows.Forms.Button();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.nudFilter = new System.Windows.Forms.NumericUpDown();
            this.btnReadLog = new System.Windows.Forms.Button();
            this.nudLogSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txbBatch = new System.Windows.Forms.TextBox();
            this.txbOperatingSite = new System.Windows.Forms.TextBox();
            this.txbUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvVariables = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStopResource = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTestResult = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStopTest = new System.Windows.Forms.Button();
            this.lblInstructionDescription = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSchedulerStepDone = new System.Windows.Forms.Label();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFolderSelected = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblTestSelected = new System.Windows.Forms.Label();
            this.btnSelectTest = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgBasic = new System.Windows.Forms.TabPage();
            this.btnStopTestProgram = new System.Windows.Forms.Button();
            this.btnStartTestProgram = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSelectResultFolder = new System.Windows.Forms.Button();
            this.btnSelectTestPlan = new System.Windows.Forms.Button();
            this.lblCanResourceStatus = new System.Windows.Forms.Label();
            this.ledResourceStatus = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.btnStopCanResource = new System.Windows.Forms.Button();
            this.btnStartCanResource = new System.Windows.Forms.Button();
            this.tpgAdvanced = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ledTestLoaded = new System.Windows.Forms.Panel();
            this.ledResultFolderSelected = new System.Windows.Forms.Panel();
            this.lblBasicTestResult = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblBasicInstruction = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblBasicStepNumber = new System.Windows.Forms.Label();
            this.btnCheckFileIntegrity = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSize)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVariables)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgBasic.SuspendLayout();
            this.tpgAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbTestLog
            // 
            this.txbTestLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.txbTestLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTestLog.ForeColor = System.Drawing.SystemColors.Info;
            this.txbTestLog.Location = new System.Drawing.Point(12, 441);
            this.txbTestLog.Multiline = true;
            this.txbTestLog.Name = "txbTestLog";
            this.txbTestLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbTestLog.Size = new System.Drawing.Size(1323, 349);
            this.txbTestLog.TabIndex = 0;
            // 
            // btnStartResource
            // 
            this.btnStartResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartResource.Location = new System.Drawing.Point(6, 19);
            this.btnStartResource.Name = "btnStartResource";
            this.btnStartResource.Size = new System.Drawing.Size(190, 23);
            this.btnStartResource.TabIndex = 2;
            this.btnStartResource.Text = "Start";
            this.btnStartResource.UseVisualStyleBackColor = true;
            this.btnStartResource.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // lblResourceStatus
            // 
            this.lblResourceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResourceStatus.Location = new System.Drawing.Point(96, 50);
            this.lblResourceStatus.Name = "lblResourceStatus";
            this.lblResourceStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblResourceStatus.Size = new System.Drawing.Size(151, 19);
            this.lblResourceStatus.TabIndex = 3;
            this.lblResourceStatus.Text = "00000000";
            this.lblResourceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlResourceStarted
            // 
            this.pnlResourceStarted.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlResourceStarted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResourceStarted.Location = new System.Drawing.Point(231, 31);
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
            this.chbLogEnabled.CheckedChanged += new System.EventHandler(this.CbxLogEnabled_CheckedChanged);
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
            // lbxFilteredCanId
            // 
            this.lbxFilteredCanId.FormattingEnabled = true;
            this.lbxFilteredCanId.Location = new System.Drawing.Point(304, 14);
            this.lbxFilteredCanId.Name = "lbxFilteredCanId";
            this.lbxFilteredCanId.Size = new System.Drawing.Size(155, 121);
            this.lbxFilteredCanId.TabIndex = 1;
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFilter.Location = new System.Drawing.Point(12, 87);
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
            this.btnAddFilter.Location = new System.Drawing.Point(12, 58);
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
            this.label10.Location = new System.Drawing.Point(10, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Filtered CAN id (hexadecimal):";
            // 
            // nudFilter
            // 
            this.nudFilter.Hexadecimal = true;
            this.nudFilter.Location = new System.Drawing.Point(211, 35);
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
            this.btnReadLog.Location = new System.Drawing.Point(12, 116);
            this.btnReadLog.Name = "btnReadLog";
            this.btnReadLog.Size = new System.Drawing.Size(286, 23);
            this.btnReadLog.TabIndex = 10;
            this.btnReadLog.Text = "Read log";
            this.btnReadLog.UseVisualStyleBackColor = true;
            // 
            // nudLogSize
            // 
            this.nudLogSize.Location = new System.Drawing.Point(211, 14);
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(938, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 12;
            this.label12.Text = "Batch";
            // 
            // txbBatch
            // 
            this.txbBatch.Location = new System.Drawing.Point(995, 21);
            this.txbBatch.Name = "txbBatch";
            this.txbBatch.Size = new System.Drawing.Size(317, 26);
            this.txbBatch.TabIndex = 11;
            // 
            // txbOperatingSite
            // 
            this.txbOperatingSite.Location = new System.Drawing.Point(587, 21);
            this.txbOperatingSite.Name = "txbOperatingSite";
            this.txbOperatingSite.Size = new System.Drawing.Size(345, 26);
            this.txbOperatingSite.TabIndex = 10;
            // 
            // txbUser
            // 
            this.txbUser.Location = new System.Drawing.Point(54, 21);
            this.txbUser.Name = "txbUser";
            this.txbUser.Size = new System.Drawing.Size(407, 26);
            this.txbUser.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(467, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Production site";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 20);
            this.label11.TabIndex = 7;
            this.label11.Text = "User";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(347, 17);
            this.cbxBaudRate.Margin = new System.Windows.Forms.Padding(2);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(112, 21);
            this.cbxBaudRate.TabIndex = 1;
            this.cbxBaudRate.SelectedIndexChanged += new System.EventHandler(this.CbxBaudRate_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txbBatch);
            this.groupBox5.Controls.Add(this.txbOperatingSite);
            this.groupBox5.Controls.Add(this.txbUser);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(16, 11);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(1321, 53);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Test informations";
            // 
            // dgvVariables
            // 
            this.dgvVariables.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVariables.Location = new System.Drawing.Point(734, 5);
            this.dgvVariables.Name = "dgvVariables";
            this.dgvVariables.Size = new System.Drawing.Size(578, 320);
            this.dgvVariables.TabIndex = 29;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbxBaudRate);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cbxDeviceList);
            this.groupBox4.Location = new System.Drawing.Point(5, 5);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(465, 53);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CAN resource settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(269, 20);
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
            this.cbxDeviceList.Size = new System.Drawing.Size(165, 21);
            this.cbxDeviceList.TabIndex = 0;
            this.cbxDeviceList.SelectedIndexChanged += new System.EventHandler(this.CbxDeviceList_SelectedIndexChanged);
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
            this.groupBox8.Location = new System.Drawing.Point(5, 179);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(465, 145);
            this.groupBox8.TabIndex = 28;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Log parameters";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblResourceStatus);
            this.groupBox6.Controls.Add(this.pnlResourceStarted);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.chbLogEnabled);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(217, 63);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(253, 110);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CAN resource info";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStopResource);
            this.groupBox3.Controls.Add(this.btnStartResource);
            this.groupBox3.Location = new System.Drawing.Point(5, 63);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(206, 110);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CAN resource commands";
            // 
            // btnStopResource
            // 
            this.btnStopResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopResource.Location = new System.Drawing.Point(6, 56);
            this.btnStopResource.Name = "btnStopResource";
            this.btnStopResource.Size = new System.Drawing.Size(190, 23);
            this.btnStopResource.TabIndex = 7;
            this.btnStopResource.Text = "Stop";
            this.btnStopResource.UseVisualStyleBackColor = true;
            this.btnStopResource.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTestResult);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnStopTest);
            this.groupBox2.Controls.Add(this.lblInstructionDescription);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblSchedulerStepDone);
            this.groupBox2.Controls.Add(this.btnStartTest);
            this.groupBox2.Location = new System.Drawing.Point(476, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 146);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test options";
            // 
            // lblTestResult
            // 
            this.lblTestResult.AutoSize = true;
            this.lblTestResult.Location = new System.Drawing.Point(108, 127);
            this.lblTestResult.Name = "lblTestResult";
            this.lblTestResult.Size = new System.Drawing.Size(13, 13);
            this.lblTestResult.TabIndex = 9;
            this.lblTestResult.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Test result:";
            // 
            // btnStopTest
            // 
            this.btnStopTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopTest.Location = new System.Drawing.Point(6, 58);
            this.btnStopTest.Name = "btnStopTest";
            this.btnStopTest.Size = new System.Drawing.Size(241, 23);
            this.btnStopTest.TabIndex = 7;
            this.btnStopTest.Text = "Stop test";
            this.btnStopTest.UseVisualStyleBackColor = true;
            this.btnStopTest.Click += new System.EventHandler(this.BtnStopTest_Click);
            // 
            // lblInstructionDescription
            // 
            this.lblInstructionDescription.Location = new System.Drawing.Point(113, 97);
            this.lblInstructionDescription.Name = "lblInstructionDescription";
            this.lblInstructionDescription.Size = new System.Drawing.Size(134, 34);
            this.lblInstructionDescription.TabIndex = 6;
            this.lblInstructionDescription.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Actual instruction:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Actual step number:";
            // 
            // lblSchedulerStepDone
            // 
            this.lblSchedulerStepDone.AutoSize = true;
            this.lblSchedulerStepDone.Location = new System.Drawing.Point(113, 84);
            this.lblSchedulerStepDone.Name = "lblSchedulerStepDone";
            this.lblSchedulerStepDone.Size = new System.Drawing.Size(24, 13);
            this.lblSchedulerStepDone.TabIndex = 3;
            this.lblSchedulerStepDone.Text = "--/--";
            // 
            // btnStartTest
            // 
            this.btnStartTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTest.Location = new System.Drawing.Point(6, 29);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(241, 23);
            this.btnStartTest.TabIndex = 0;
            this.btnStartTest.Text = "Start test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.BtnStartTest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckFileIntegrity);
            this.groupBox1.Controls.Add(this.lblFolderSelected);
            this.groupBox1.Controls.Add(this.btnSelectFolder);
            this.groupBox1.Controls.Add(this.lblTestSelected);
            this.groupBox1.Controls.Add(this.btnSelectTest);
            this.groupBox1.Location = new System.Drawing.Point(476, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 167);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File options";
            // 
            // lblFolderSelected
            // 
            this.lblFolderSelected.Location = new System.Drawing.Point(6, 103);
            this.lblFolderSelected.Name = "lblFolderSelected";
            this.lblFolderSelected.Size = new System.Drawing.Size(241, 33);
            this.lblFolderSelected.TabIndex = 3;
            this.lblFolderSelected.Text = "Folder selected: ----";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFolder.Location = new System.Drawing.Point(6, 77);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(241, 23);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Select report folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.BtnSelectFolder_Click);
            // 
            // lblTestSelected
            // 
            this.lblTestSelected.Location = new System.Drawing.Point(6, 45);
            this.lblTestSelected.Name = "lblTestSelected";
            this.lblTestSelected.Size = new System.Drawing.Size(241, 29);
            this.lblTestSelected.TabIndex = 1;
            this.lblTestSelected.Text = "Test selected: ----";
            // 
            // btnSelectTest
            // 
            this.btnSelectTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectTest.Location = new System.Drawing.Point(6, 19);
            this.btnSelectTest.Name = "btnSelectTest";
            this.btnSelectTest.Size = new System.Drawing.Size(241, 23);
            this.btnSelectTest.TabIndex = 0;
            this.btnSelectTest.Text = "Select test folder";
            this.btnSelectTest.UseVisualStyleBackColor = true;
            this.btnSelectTest.Click += new System.EventHandler(this.BtnSelectTest_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgBasic);
            this.tabControl1.Controls.Add(this.tpgAdvanced);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1329, 366);
            this.tabControl1.TabIndex = 31;
            // 
            // tpgBasic
            // 
            this.tpgBasic.Controls.Add(this.lblBasicTestResult);
            this.tpgBasic.Controls.Add(this.label18);
            this.tpgBasic.Controls.Add(this.lblBasicInstruction);
            this.tpgBasic.Controls.Add(this.label20);
            this.tpgBasic.Controls.Add(this.label21);
            this.tpgBasic.Controls.Add(this.lblBasicStepNumber);
            this.tpgBasic.Controls.Add(this.ledResultFolderSelected);
            this.tpgBasic.Controls.Add(this.ledTestLoaded);
            this.tpgBasic.Controls.Add(this.label16);
            this.tpgBasic.Controls.Add(this.label15);
            this.tpgBasic.Controls.Add(this.btnStopTestProgram);
            this.tpgBasic.Controls.Add(this.btnStartTestProgram);
            this.tpgBasic.Controls.Add(this.label14);
            this.tpgBasic.Controls.Add(this.btnSelectResultFolder);
            this.tpgBasic.Controls.Add(this.btnSelectTestPlan);
            this.tpgBasic.Controls.Add(this.lblCanResourceStatus);
            this.tpgBasic.Controls.Add(this.ledResourceStatus);
            this.tpgBasic.Controls.Add(this.label13);
            this.tpgBasic.Controls.Add(this.btnStopCanResource);
            this.tpgBasic.Controls.Add(this.btnStartCanResource);
            this.tpgBasic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpgBasic.Location = new System.Drawing.Point(4, 29);
            this.tpgBasic.Name = "tpgBasic";
            this.tpgBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tpgBasic.Size = new System.Drawing.Size(1321, 333);
            this.tpgBasic.TabIndex = 0;
            this.tpgBasic.Text = "Basic";
            this.tpgBasic.UseVisualStyleBackColor = true;
            // 
            // btnStopTestProgram
            // 
            this.btnStopTestProgram.Location = new System.Drawing.Point(942, 64);
            this.btnStopTestProgram.Name = "btnStopTestProgram";
            this.btnStopTestProgram.Size = new System.Drawing.Size(370, 52);
            this.btnStopTestProgram.TabIndex = 9;
            this.btnStopTestProgram.Text = "Stop test program";
            this.btnStopTestProgram.UseVisualStyleBackColor = true;
            this.btnStopTestProgram.Click += new System.EventHandler(this.BtnStopTest_Click);
            // 
            // btnStartTestProgram
            // 
            this.btnStartTestProgram.Location = new System.Drawing.Point(942, 6);
            this.btnStartTestProgram.Name = "btnStartTestProgram";
            this.btnStartTestProgram.Size = new System.Drawing.Size(370, 52);
            this.btnStartTestProgram.TabIndex = 8;
            this.btnStartTestProgram.Text = "Start test program";
            this.btnStartTestProgram.UseVisualStyleBackColor = true;
            this.btnStartTestProgram.Click += new System.EventHandler(this.BtnStartTest_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 151);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(158, 20);
            this.label14.TabIndex = 7;
            this.label14.Text = "Can resource started";
            // 
            // btnSelectResultFolder
            // 
            this.btnSelectResultFolder.Location = new System.Drawing.Point(474, 64);
            this.btnSelectResultFolder.Name = "btnSelectResultFolder";
            this.btnSelectResultFolder.Size = new System.Drawing.Size(370, 52);
            this.btnSelectResultFolder.TabIndex = 6;
            this.btnSelectResultFolder.Text = "Select result folder";
            this.btnSelectResultFolder.UseVisualStyleBackColor = true;
            this.btnSelectResultFolder.Click += new System.EventHandler(this.BtnSelectFolder_Click);
            // 
            // btnSelectTestPlan
            // 
            this.btnSelectTestPlan.Location = new System.Drawing.Point(474, 6);
            this.btnSelectTestPlan.Name = "btnSelectTestPlan";
            this.btnSelectTestPlan.Size = new System.Drawing.Size(370, 52);
            this.btnSelectTestPlan.TabIndex = 5;
            this.btnSelectTestPlan.Text = "Select test plan";
            this.btnSelectTestPlan.UseVisualStyleBackColor = true;
            this.btnSelectTestPlan.Click += new System.EventHandler(this.BtnSelectTest_Click);
            // 
            // lblCanResourceStatus
            // 
            this.lblCanResourceStatus.AutoSize = true;
            this.lblCanResourceStatus.Location = new System.Drawing.Point(57, 219);
            this.lblCanResourceStatus.Name = "lblCanResourceStatus";
            this.lblCanResourceStatus.Size = new System.Drawing.Size(29, 20);
            this.lblCanResourceStatus.TabIndex = 4;
            this.lblCanResourceStatus.Text = "----";
            // 
            // ledResourceStatus
            // 
            this.ledResourceStatus.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ledResourceStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledResourceStatus.Location = new System.Drawing.Point(287, 143);
            this.ledResourceStatus.Name = "ledResourceStatus";
            this.ledResourceStatus.Size = new System.Drawing.Size(32, 32);
            this.ledResourceStatus.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(156, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "Can resource status:";
            // 
            // btnStopCanResource
            // 
            this.btnStopCanResource.Location = new System.Drawing.Point(6, 64);
            this.btnStopCanResource.Name = "btnStopCanResource";
            this.btnStopCanResource.Size = new System.Drawing.Size(370, 52);
            this.btnStopCanResource.TabIndex = 1;
            this.btnStopCanResource.Text = "Stop can resource";
            this.btnStopCanResource.UseVisualStyleBackColor = true;
            this.btnStopCanResource.Click += new System.EventHandler(this.BtnStopCanResource_Click);
            // 
            // btnStartCanResource
            // 
            this.btnStartCanResource.Location = new System.Drawing.Point(6, 6);
            this.btnStartCanResource.Name = "btnStartCanResource";
            this.btnStartCanResource.Size = new System.Drawing.Size(370, 52);
            this.btnStartCanResource.TabIndex = 0;
            this.btnStartCanResource.Text = "Start can resource";
            this.btnStartCanResource.UseVisualStyleBackColor = true;
            this.btnStartCanResource.Click += new System.EventHandler(this.BtnStartCanResource_Click);
            // 
            // tpgAdvanced
            // 
            this.tpgAdvanced.Controls.Add(this.dgvVariables);
            this.tpgAdvanced.Controls.Add(this.groupBox4);
            this.tpgAdvanced.Controls.Add(this.groupBox2);
            this.tpgAdvanced.Controls.Add(this.groupBox8);
            this.tpgAdvanced.Controls.Add(this.groupBox3);
            this.tpgAdvanced.Controls.Add(this.groupBox6);
            this.tpgAdvanced.Controls.Add(this.groupBox1);
            this.tpgAdvanced.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpgAdvanced.Location = new System.Drawing.Point(4, 29);
            this.tpgAdvanced.Name = "tpgAdvanced";
            this.tpgAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tpgAdvanced.Size = new System.Drawing.Size(1321, 333);
            this.tpgAdvanced.TabIndex = 1;
            this.tpgAdvanced.Text = "Advanced";
            this.tpgAdvanced.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(470, 151);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(155, 20);
            this.label15.TabIndex = 10;
            this.label15.Text = "Test program loaded";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(470, 199);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(191, 20);
            this.label16.TabIndex = 11;
            this.label16.Text = "Test result folder selected";
            // 
            // ledTestLoaded
            // 
            this.ledTestLoaded.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ledTestLoaded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledTestLoaded.Location = new System.Drawing.Point(812, 143);
            this.ledTestLoaded.Name = "ledTestLoaded";
            this.ledTestLoaded.Size = new System.Drawing.Size(32, 32);
            this.ledTestLoaded.TabIndex = 4;
            // 
            // ledResultFolderSelected
            // 
            this.ledResultFolderSelected.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ledResultFolderSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledResultFolderSelected.Location = new System.Drawing.Point(812, 193);
            this.ledResultFolderSelected.Name = "ledResultFolderSelected";
            this.ledResultFolderSelected.Size = new System.Drawing.Size(32, 32);
            this.ledResultFolderSelected.TabIndex = 4;
            // 
            // lblBasicTestResult
            // 
            this.lblBasicTestResult.AutoSize = true;
            this.lblBasicTestResult.Location = new System.Drawing.Point(1119, 275);
            this.lblBasicTestResult.Name = "lblBasicTestResult";
            this.lblBasicTestResult.Size = new System.Drawing.Size(19, 20);
            this.lblBasicTestResult.TabIndex = 17;
            this.lblBasicTestResult.Text = "--";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(946, 275);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 20);
            this.label18.TabIndex = 16;
            this.label18.Text = "Test result:";
            // 
            // lblBasicInstruction
            // 
            this.lblBasicInstruction.Location = new System.Drawing.Point(1119, 199);
            this.lblBasicInstruction.Name = "lblBasicInstruction";
            this.lblBasicInstruction.Size = new System.Drawing.Size(193, 54);
            this.lblBasicInstruction.TabIndex = 15;
            this.lblBasicInstruction.Text = "--";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(946, 199);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(135, 20);
            this.label20.TabIndex = 14;
            this.label20.Text = "Actual instruction:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(946, 151);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(151, 20);
            this.label21.TabIndex = 13;
            this.label21.Text = "Actual step number:";
            // 
            // lblBasicStepNumber
            // 
            this.lblBasicStepNumber.AutoSize = true;
            this.lblBasicStepNumber.Location = new System.Drawing.Point(1115, 151);
            this.lblBasicStepNumber.Name = "lblBasicStepNumber";
            this.lblBasicStepNumber.Size = new System.Drawing.Size(33, 20);
            this.lblBasicStepNumber.TabIndex = 12;
            this.lblBasicStepNumber.Text = "--/--";
            // 
            // btnCheckFileIntegrity
            // 
            this.btnCheckFileIntegrity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckFileIntegrity.Location = new System.Drawing.Point(6, 139);
            this.btnCheckFileIntegrity.Name = "btnCheckFileIntegrity";
            this.btnCheckFileIntegrity.Size = new System.Drawing.Size(241, 23);
            this.btnCheckFileIntegrity.TabIndex = 4;
            this.btnCheckFileIntegrity.Text = "Check file integrity";
            this.btnCheckFileIntegrity.UseVisualStyleBackColor = true;
            this.btnCheckFileIntegrity.Click += new System.EventHandler(this.BtnCheckFileIntegrity_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 802);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txbTestLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "AQ test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSize)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVariables)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpgBasic.ResumeLayout(false);
            this.tpgBasic.PerformLayout();
            this.tpgAdvanced.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbTestLog;
        private System.Windows.Forms.Button btnStartResource;
        private System.Windows.Forms.Label lblResourceStatus;
        private System.Windows.Forms.Panel pnlResourceStarted;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chbLogEnabled;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lbxFilteredCanId;
        private System.Windows.Forms.Button btnRemoveFilter;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudFilter;
        private System.Windows.Forms.Button btnReadLog;
        private System.Windows.Forms.NumericUpDown nudLogSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txbBatch;
        private System.Windows.Forms.TextBox txbOperatingSite;
        private System.Windows.Forms.TextBox txbUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvVariables;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxDeviceList;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStopResource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTestResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStopTest;
        private System.Windows.Forms.Label lblInstructionDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSchedulerStepDone;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFolderSelected;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblTestSelected;
        private System.Windows.Forms.Button btnSelectTest;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgBasic;
        private System.Windows.Forms.TabPage tpgAdvanced;
        private System.Windows.Forms.Label lblCanResourceStatus;
        private System.Windows.Forms.Panel ledResourceStatus;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnStopCanResource;
        private System.Windows.Forms.Button btnStartCanResource;
        private System.Windows.Forms.Button btnSelectResultFolder;
        private System.Windows.Forms.Button btnSelectTestPlan;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnStopTestProgram;
        private System.Windows.Forms.Button btnStartTestProgram;
        private System.Windows.Forms.Panel ledResultFolderSelected;
        private System.Windows.Forms.Panel ledTestLoaded;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblBasicTestResult;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblBasicInstruction;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblBasicStepNumber;
        private System.Windows.Forms.Button btnCheckFileIntegrity;
    }
}

