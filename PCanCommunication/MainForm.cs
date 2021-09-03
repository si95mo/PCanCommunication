using Hardware.Can;
using Hardware.Can.Peak.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCanCommunication
{
    /// <summary>
    /// Implement a visual application for managing the can communication
    /// </summary>
    public partial class MainForm : Form
    {
        // Color constants
        private readonly Color startedColor = Color.Green;
        private readonly Color stoppedColor = Color.Red;
        private readonly Color unknowkColor = Color.DarkGray;

        // Can-related variables
        private ushort hardwareHandle = 0;
        private PeakCanResource resource; // The resource

        private CanChannel actualResistance; // R act channel
        private CanChannel setResistance; // R set channel

        // The initial time (first x axis value of the chart)
        private TimeSpan startTime;

        // Chart-related variables
        private bool continueToUpdateChart = true;

        private int chartLineTickness = 2;
        private readonly int numberOfPOints = 32;
        private readonly int updateInterval = 100; // ms

        // Utility
        private readonly string rSetName = "Rset"; // Chart series name

        private readonly string rActName = "Ract"; // Chart series name

        // Converted values
        private double rAct = 0.0;
        private double rSet = 0.0;

        /// <summary>
        /// Convert a textual representation of a <see cref="BaudRate"/>
        /// </summary>
        /// <param name="baudRate">The baud rate as <see cref="string"/></param>
        /// <returns>The baudrate as <see cref="BaudRate"/></returns>
        private BaudRate StringToBaudRate(string baudRate)
        {
            BaudRate convertedBaudRate;

            switch(baudRate)
            {
                case "1000 kbit/s":
                    convertedBaudRate = BaudRate.K1000;
                    break;
                case "800 kbit/s":
                    convertedBaudRate = BaudRate.K800;
                    break;
                case "500 kbit/s":
                    convertedBaudRate = BaudRate.K500;
                    break;
                case "250 kbit/s":
                    convertedBaudRate = BaudRate.K250;
                    break;
                case "125 kbit/s":
                    convertedBaudRate = BaudRate.K125;
                    break;
                case "100 kbit/s":
                    convertedBaudRate = BaudRate.K100;
                    break;
                case "95 kbit/s":
                    convertedBaudRate = BaudRate.K95;
                    break;
                case "83 kbit/s":
                    convertedBaudRate = BaudRate.K83;
                    break;
                case "50 kbit/s":
                    convertedBaudRate = BaudRate.K50;
                    break;
                case "47 kbit/s":
                    convertedBaudRate = BaudRate.K47;
                    break;
                case "33 kbit/s":
                    convertedBaudRate = BaudRate.K33;
                    break;
                case "20 kbit/s":
                    convertedBaudRate = BaudRate.K20;
                    break;
                case "10 kbit/s":
                    convertedBaudRate = BaudRate.K10;
                    break;
                case "5 kbit/s":
                    convertedBaudRate = BaudRate.K5;
                    break;
                default:
                    convertedBaudRate = BaudRate.K500;
                    break;
            }

            return convertedBaudRate;
        }

        /// <summary>
        /// Initialize the chart
        /// </summary>
        private void InitializeChart()
        {
            // Remove old series
            crtVariables.Series.Clear();

            // Add the new series
            crtVariables.Series.Add(rSetName);
            crtVariables.Series.Add(rActName);

            // Initialize the new added series
            crtVariables.Series[rSetName].ChartType = SeriesChartType.FastLine;
            crtVariables.Series[rSetName].BorderWidth = chartLineTickness;
            crtVariables.Series[rSetName].YAxisType = AxisType.Primary;
            crtVariables.Series[rSetName].Color = Color.OrangeRed;

            crtVariables.Series[rActName].ChartType = SeriesChartType.FastLine;
            crtVariables.Series[rActName].BorderWidth = chartLineTickness;
            crtVariables.Series[rActName].YAxisType = AxisType.Secondary;
            crtVariables.Series[rActName].Color = Color.RoyalBlue;

            // Initialize the chart axis
            crtVariables.ChartAreas[0].AxisX.Title = "Time [ms]";
            crtVariables.ChartAreas[0].AxisY.Title = "Rset [Ohm]";
            crtVariables.ChartAreas[0].AxisY2.Title = "Ract [Ohm]";

            crtVariables.ChartAreas[0].AxisX.Minimum = 0;
        }

        /// <summary>
        /// Initialize the chart (async) task updater
        /// </summary>
        private void InitializeChartUpdater()
        {
            // Start the background (async) task
            bgWorker.DoWork += ChartUpdater_DoWork;
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Initialize user interface-related components
        /// </summary>
        private void InitializeUserInterface()
        {
            // Set the resource started led to a default color
            pnlResourceStarted.BackColor = unknowkColor;
            // Set the log button border color to stopped
            btnReadLog.FlatAppearance.BorderColor = startedColor;

            List<string> hardwareNames = PeakCanResource.GetAvailableHardware();

            BindingSource bs = new BindingSource();
            bs.DataSource = hardwareNames;

            cbxDeviceList.DataSource = bs.DataSource;
            cbxDeviceList.SelectedIndex = 0;

            bs = new BindingSource();
            bs.DataSource = new string[]
            {
                "1000 kbit/s",
                "800 kbit/s",
                "500 kbit/s",
                "250 kbit/s",
                "125 kbit/s",
                "100 kbit/s",
                "95 kbit/s",
                "83 kbit/s",
                "50 kbit/s",
                "47 kbit/s",
                "33 kbit/s",
                "20 kbit/s",
                "10 kbit/s",
                "5 kbit/s"
            };
            cbxBaudRate.DataSource = bs;

            cbxBaudRate.SelectedIndex = 2; // 500 kbit/s
            cbxBaudRate.SelectedIndexChanged += CbxBaudRate_SelectedIndexChanged;
        }

        /// <summary>
        /// Initialize the can-related objects
        /// (like the <see cref="PeakCanResource"/> or the
        /// various <see cref="CanChannel"/>)
        /// </summary>
        private void InitializeCanCommunication()
        {
            // Create the can reasource
            resource = new PeakCanResource(hardwareHandle, StringToBaudRate(cbxBaudRate.SelectedItem.ToString()));

            // Update the UI and connect the event handler
            lblResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();
            resource.StatusChanged += Resource_StatusChanged;

            if (resource.Status != 0) // 0 is for no error
                lblResourceStatus.ForeColor = stoppedColor;
            else
                lblResourceStatus.ForeColor = startedColor;

            // Create the can channels
            actualResistance = new CanChannel(0x180, resource);
            setResistance = new CanChannel(0x200, resource);

            // Connect the event handlers
            actualResistance.DataChanged += ActualResistance_DataChanged;
            setResistance.DataChanged += SetResistance_DataChanged;

            // Initialize the can channels data to a default value
            actualResistance.Data = new byte[]
            {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0};
            setResistance.Data = new byte[]
            {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0};

            // Subscribe the channels to the resource
            resource.Channels.Add(actualResistance);
            resource.Channels.Add(setResistance);

            resource.AddFilteredCanId(actualResistance.CanId);
            resource.AddFilteredCanId(setResistance.CanId);
        }

        private void SetResistance_DataChanged(object sender, DataChangedEventArgs e)
        {
            lblSetValue.Invoke(new MethodInvoker(() =>
                    {
                        // lblSetValue.Text = $"{BitConverter.ToDouble(setResistance.Data, 0):F3} Ohm";
                        rSet = ByteArrayToDouble(setResistance.Data, actual: false);
                        lblSetValue.Text = $"{rSet:F3} Ohm";
                    }
                )
            );
        }

        private void ActualResistance_DataChanged(object sender, DataChangedEventArgs e)
        {
            lblActualValue.Invoke(new MethodInvoker(() =>
                    {
                        // lblActualValue.Text = string.Join(", ", actualResistance.Data);
                        rAct = ByteArrayToDouble(actualResistance.Data, actual: true);
                        lblActualValue.Text = $"{rAct:F3} Ohm";
                    }
                )
            );
        }

        // Handle the resource status value changed
        private void Resource_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Update the UI component (different thread)
            lblResourceStatus.Invoke(new MethodInvoker(() =>
                    {
                        lblResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();

                        if (lblResourceStatus.Text.CompareTo("PCAN_ERROR_OK") != 0) // PCAN_ERROR_OK is fo no error
                            lblResourceStatus.ForeColor = Color.Red;
                        else
                            lblResourceStatus.ForeColor = Color.Black;
                    }
                )
            );
        }

        /// <summary>
        /// Initialize a new instance of <see cref="MainForm"/>
        /// </summary>
        public MainForm()
        {
            // Initialize basic UI components
            InitializeComponent();

            // Initialize chart
            InitializeChart();
        }

        // Handle initialization that can only be done
        // after the form control has been created
        // (in particular the can communication initialization
        // because there is an Invoke method call inside)
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize other UI components
            InitializeUserInterface();

            // Initialize can-related objects
            // This operation is performed in the Load event because
            // a call to the Invoke method is performed in the code below
            InitializeCanCommunication();

            // Initialize chart task updater
            InitializeChartUpdater();

            // Update the filtered can id
            UpdateFiltereCanId();
        }

        // Update the chart
        private async void ChartUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            // Chart x axis value
            int x = 0;
            startTime = new TimeSpan(DateTime.Now.Ticks); // Initial time

            while (continueToUpdateChart)
            {
                x = (int)(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds - startTime.TotalMilliseconds);

                // Dummy values, have to be replaced with the values read via can protocol
                // (Rset is an asynchronous read, sent back once when a different value is written,
                // while Ract is a periodic read - the frame is automatically sent
                // by the board in the can bus every x milliseconds)
                //double a = -1 + 0.02 * Math.Cos(x);
                //double s = 1 + 0.02 * Math.Sin(x);
                // actualResistance.CanFrame.Data = BitConverter.GetBytes(a);
                // setResistance.CanFrame.Data = BitConverter.GetBytes(s);
                //actualResistance.Data = BitConverter.GetBytes(a);
                //setResistance.Data = BitConverter.GetBytes(s);

                crtVariables.Invoke(new MethodInvoker(() => UpdateChart(x)));

                // Wait for an amount of time
                await Task.Delay(updateInterval);
            }
        }

        /// <summary>
        /// Update the chart (intended as the UI component)
        /// </summary>
        /// <param name="x">The x axis value to add</param>
        private void UpdateChart(int x)
        {
            // Add the new points to the chart series
            crtVariables.Series[rSetName].Points.AddXY(x, rSet);
            crtVariables.Series[rActName].Points.AddXY(x, rAct);

            // "Real-time" chart, the oldest point is removed if necessary
            if (crtVariables.Series[rSetName].Points.Count > numberOfPOints)
            {
                // Remove first series point
                crtVariables.Series[rSetName].Points.RemoveAt(0);
                // Remove second series point
                crtVariables.Series[rActName].Points.RemoveAt(0);

                // Update chart
                crtVariables.ChartAreas[0].AxisX.Minimum = crtVariables.Series[rSetName].Points[0].XValue;
                crtVariables.ResetAutoValues();
            }
        }

        // Handle the start button click
        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Start the resource
            resource?.Start();
            // Enable the log
            resource?.EnableLog();

            // Update UI
            pnlResourceStarted.BackColor = startedColor;
        }

        // Handle the stop button click
        private void BtnStop_Click(object sender, EventArgs e)
        {
            // Stop the resource.
            // Doesn't disable the log in order to not delete unseen log entries
            // (multiple invokes of EnableLog will delete the log)
            resource?.Stop(); 

            // Update UI
            pnlResourceStarted.BackColor = stoppedColor;
        }

        // Handle the read log button click
        private void BtnReadLog_Click(object sender, EventArgs e)
            => txbLog.Text = resource.ReadLog();

        // Handle the form closing event
        // (disable the chart updating and stop the can resource)
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop to update the chart
            continueToUpdateChart = false;
            // Stop the can resource
            resource?.Stop();
        }

        private void btnSetVariables_Click(object sender, EventArgs e)
        {
            // Parse the user input for R set
            bool flag = int.TryParse(txbRSet.Text.Replace('.', ','), out int value);

            if (flag)
            {
                setResistance.Data = IntToByteArray(value);
            }
            else
            {
                MessageBox.Show(
                    "Please, insert a valid value!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txbRSet.Focus();
            }
        }

        private void CbxLogEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chbLogEnabled.Checked)
            {
                // Enable log
                resource.EnableLog((int)nudLogSize.Value);
                // And update UI
                btnReadLog.FlatAppearance.BorderColor = startedColor;
            }
            else
            {
                // Disable log
                resource.DisableLog();
                // And update UI
                btnReadLog.FlatAppearance.BorderColor = stoppedColor;
            }
        }

        private void CbxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
            => resource?.SetBaudRate(StringToBaudRate(cbxBaudRate.SelectedItem.ToString())); 

        /// <summary>
        /// Convert a <see cref="byte"/> array in little endian
        /// to big endian and then convert it in in <see cref="double"/>
        /// </summary>
        /// <param name="data">The data convert</param>
        /// <param name="actual">Indicates whether convert the actual or set resistance</param>
        /// <returns>The converted data</returns>
        private double ByteArrayToDouble(byte[] data, bool actual = true)
        {
            double converted = 0.0;

            byte[] storedData = new byte[4];

            if (actual)
            {
                for (int i = 0; i < 4; i++)
                    storedData[i] = data[4 + i];
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    storedData[i] = data[4 + i];
            }

            converted = BitConverter.ToInt32(storedData, 0);

            return converted;
        }

        private byte[] IntToByteArray(int data)
        {
            byte[] converted = new byte[8];

            byte[] partial = BitConverter.GetBytes(data);

            for(int i = 0; i < 8; i++)
            {
                if (i < 4)
                    converted[i] = 0x0;
                else
                    converted[i] = partial[i - 4];
            }

            return converted;
        }

        private void btnSetCanId_Click(object sender, EventArgs e)
        {
            actualResistance.CanId = 0x180 + (int)nudReceive.Value;
            setResistance.CanId = 0x200 + (int)nudSend.Value;
        }

        /// <summary>
        /// Update the UI component with the updated
        /// filtered can id
        /// </summary>
        private void UpdateFiltereCanId()
        {
            Dictionary<int, bool> ids = resource?.FilteredCanId;

            lbxFilteredCanId.Items.Clear();
            foreach (KeyValuePair<int, bool> id in ids)
            {
                if (id.Value)
                    lbxFilteredCanId.Items.Add($"0x{id.Key:X2}");
            }
        }

        private void BtnAddFiltered(object sender, EventArgs e)
        {
            resource?.AddFilteredCanId((int)nudFilter.Value);
            UpdateFiltereCanId();            
        }

        private void BtnRemoveFilter_Click(object sender, EventArgs e)
        {
            resource?.RemoveFilteredCanId((int)nudFilter.Value);
            UpdateFiltereCanId();
        }

        private void CbxDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cbxDeviceList.Text;
            str = str.Substring(str.IndexOf('(') + 1, 3);
            str = str.Replace('h', ' ').Trim(' ');

            hardwareHandle = Convert.ToUInt16(str, 16);
        }

        private void TxbLog_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point position = MousePosition;
                cmsClearLog.Show(position);
            }
        }

        private void CmsClearLog_Click(object sender, EventArgs e)
            => txbLog.Text = "";
    }
}