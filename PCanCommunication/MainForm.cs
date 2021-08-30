using Hardware.Can;
using Hardware.Can.Peak.Lib;
using System;
using System.ComponentModel;
using System.Drawing;
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
        private PeakCanResource resource;
        private CanChannel actualResistance;
        private CanChannel setResistance;

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
            btnReadLog.FlatAppearance.BorderColor = stoppedColor;
        }

        /// <summary>
        /// Initialize the can-related objects
        /// (like the <see cref="PeakCanResource"/> or the
        /// various <see cref="CanChannel"/>)
        /// </summary>
        private void InitializeCanCommunication()
        {
            // Create the can reasource
            resource = new PeakCanResource();

            // Update the UI and connect the event handler
            lblResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();
            resource.StatusChanged += Resource_StatusChanged;

            // Create the can channels
            actualResistance = new CanChannel(0x0, resource);
            setResistance = new CanChannel(0x1, resource);

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
        }

        private void SetResistance_DataChanged(object sender, DataChangedEventArgs e)
        {
            lblSetValue.Invoke(new MethodInvoker(() =>
                    {
                        lblSetValue.Text = $"{BitConverter.ToDouble(setResistance.Data, 0):F3} Ohm";
                        lbxLog.Items.Add($"Sent >> {setResistance}");
                    }
                )
            );
        }

        private void ActualResistance_DataChanged(object sender, DataChangedEventArgs e)
        {
            lblActualValue.Invoke(new MethodInvoker(() =>
                    {
                        lblActualValue.Text = $"{BitConverter.ToDouble(actualResistance.Data, 0):F3} Ohm";
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

                        if (lblResourceStatus.Text.CompareTo("PCAN_ERROR_OK") != 0)
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

            // Initialize other UI components
            InitializeUserInterface();

            // Initialize chart
            InitializeChart();
        }

        // Handle initialization that can only be done
        // after the form control has been created
        // (in particular the can communication initialization
        // because there is an Invoke method call inside)
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize can-related objects
            // This operation is performed in the Load event because
            // a call to the Invoke method is performed in the code below
            InitializeCanCommunication();

            // Initialize chart task updater
            InitializeChartUpdater();
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
                double a = -1 + 0.02 * Math.Cos(x);
                double s = 1 + 0.02 * Math.Sin(x);
                actualResistance.CanFrame.Data = BitConverter.GetBytes(a);
                setResistance.CanFrame.Data = BitConverter.GetBytes(s);

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
            crtVariables.Series[rSetName].Points.AddXY(x, BitConverter.ToDouble(setResistance.CanFrame.Data, 0));
            crtVariables.Series[rActName].Points.AddXY(x, BitConverter.ToDouble(actualResistance.CanFrame.Data, 0));

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
            // Update UI
            pnlResourceStarted.BackColor = startedColor;
        }

        // Handle the stop button click
        private void BtnStop_Click(object sender, EventArgs e)
        {
            resource?.Stop(); // Stop the resource
            // Don't disable the log in order to not delete unseen log entries
            // (multiple EnableLog will delete the log)

            // Update UI
            pnlResourceStarted.BackColor = stoppedColor;
        }

        // Handle the read log button click
        private void BtnReadLog_Click(object sender, EventArgs e)
        {
            lbxLog.Items.Clear(); // Clear previous log first
            // Read the log and add it to the UI component
            lbxLog.Items.Add(resource.ReadLog());
        }

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
            bool flag = double.TryParse(txbRSet.Text.Replace('.', ','), out double value);

            if (flag)
                setResistance.Data = BitConverter.GetBytes(value);
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
                resource.EnableLog(65535);
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
    }
}