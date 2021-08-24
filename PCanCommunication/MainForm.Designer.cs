
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlRAct = new System.Windows.Forms.Panel();
            this.pnlRSet = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txbRAct = new System.Windows.Forms.TextBox();
            this.txbRSet = new System.Windows.Forms.TextBox();
            this.btnSetVariables = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.crtVariables = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pnlRAct.SuspendLayout();
            this.pnlRSet.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "act";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "R";
            // 
            // pnlRAct
            // 
            this.pnlRAct.Controls.Add(this.label2);
            this.pnlRAct.Controls.Add(this.label1);
            this.pnlRAct.Location = new System.Drawing.Point(8, 38);
            this.pnlRAct.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRAct.Name = "pnlRAct";
            this.pnlRAct.Size = new System.Drawing.Size(38, 24);
            this.pnlRAct.TabIndex = 4;
            // 
            // pnlRSet
            // 
            this.pnlRSet.Controls.Add(this.label3);
            this.pnlRSet.Controls.Add(this.label4);
            this.pnlRSet.Location = new System.Drawing.Point(8, 13);
            this.pnlRSet.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRSet.Name = "pnlRSet";
            this.pnlRSet.Size = new System.Drawing.Size(38, 25);
            this.pnlRSet.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "set";
            // 
            // cbxDeviceList
            // 
            this.cbxDeviceList.FormattingEnabled = true;
            this.cbxDeviceList.Location = new System.Drawing.Point(68, 17);
            this.cbxDeviceList.Margin = new System.Windows.Forms.Padding(2);
            this.cbxDeviceList.Name = "cbxDeviceList";
            this.cbxDeviceList.Size = new System.Drawing.Size(128, 21);
            this.cbxDeviceList.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxDeviceList);
            this.groupBox1.Location = new System.Drawing.Point(9, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(200, 71);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAN settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Device list";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Baud rate";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(68, 42);
            this.cbxBaudRate.Margin = new System.Windows.Forms.Padding(2);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(128, 21);
            this.cbxBaudRate.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Location = new System.Drawing.Point(9, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 78);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CAN commands";
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Location = new System.Drawing.Point(6, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(190, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(6, 48);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(189, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSetVariables);
            this.groupBox3.Controls.Add(this.txbRSet);
            this.groupBox3.Controls.Add(this.txbRAct);
            this.groupBox3.Controls.Add(this.pnlRAct);
            this.groupBox3.Controls.Add(this.pnlRSet);
            this.groupBox3.Location = new System.Drawing.Point(9, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 114);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CAN variables";
            // 
            // txbRAct
            // 
            this.txbRAct.Location = new System.Drawing.Point(68, 40);
            this.txbRAct.Name = "txbRAct";
            this.txbRAct.Size = new System.Drawing.Size(129, 20);
            this.txbRAct.TabIndex = 6;
            // 
            // txbRSet
            // 
            this.txbRSet.Location = new System.Drawing.Point(68, 15);
            this.txbRSet.Name = "txbRSet";
            this.txbRSet.Size = new System.Drawing.Size(129, 20);
            this.txbRSet.TabIndex = 7;
            // 
            // btnSetVariables
            // 
            this.btnSetVariables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetVariables.Location = new System.Drawing.Point(8, 85);
            this.btnSetVariables.Name = "btnSetVariables";
            this.btnSetVariables.Size = new System.Drawing.Size(189, 23);
            this.btnSetVariables.TabIndex = 2;
            this.btnSetVariables.Text = "Set";
            this.btnSetVariables.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.crtVariables);
            this.groupBox4.Location = new System.Drawing.Point(215, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(957, 274);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CAN variables plot";
            // 
            // crtVariables
            // 
            this.crtVariables.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea";
            this.crtVariables.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend";
            this.crtVariables.Legends.Add(legend1);
            this.crtVariables.Location = new System.Drawing.Point(7, 20);
            this.crtVariables.Name = "crtVariables";
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend";
            series1.Name = "ActSeries";
            series2.ChartArea = "ChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend";
            series2.Name = "SetSeries";
            this.crtVariables.Series.Add(series1);
            this.crtVariables.Series.Add(series2);
            this.crtVariables.Size = new System.Drawing.Size(944, 248);
            this.crtVariables.TabIndex = 0;
            this.crtVariables.Text = "CAN variables";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 291);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "PCan communication";
            this.pnlRAct.ResumeLayout(false);
            this.pnlRAct.PerformLayout();
            this.pnlRSet.ResumeLayout(false);
            this.pnlRSet.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crtVariables)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlRAct;
        private System.Windows.Forms.Panel pnlRSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDeviceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSetVariables;
        private System.Windows.Forms.TextBox txbRSet;
        private System.Windows.Forms.TextBox txbRAct;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtVariables;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}

