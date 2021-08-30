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
    public partial class MainForm : Form
    {
        private readonly Color startedColor = Color.Green;
        private readonly Color stoppedColor = Color.Red;
        private readonly Color unknowkColor = Color.DarkGray;

        private PeakCanResource resource;
        private CanChannel actualResistance;
        private CanChannel setResistance;

        private TimeSpan startTime;

        private bool continueToUpdateChart = true;
        private readonly int numberOfPOints = 32;
        private readonly int updateInterval = 100; // ms

        private double rSet;
        private double rAct;

        private readonly string rSetName = "Rset";
        private readonly string rActName = "Ract";

        /// <summary>
        /// Initialize the chart
        /// </summary>
        private void InitializeChart()
        {
            crtVariables.Series.Clear();

            crtVariables.Series.Add(rSetName);
            crtVariables.Series.Add(rActName);

            crtVariables.Series[rSetName].ChartType = SeriesChartType.FastLine;
            crtVariables.Series[rSetName].YAxisType = AxisType.Primary;
            crtVariables.Series[rSetName].Color = Color.OrangeRed;

            crtVariables.Series[rActName].ChartType = SeriesChartType.FastLine;
            crtVariables.Series[rActName].YAxisType = AxisType.Secondary;
            crtVariables.Series[rActName].Color = Color.RoyalBlue;

            crtVariables.ChartAreas[0].AxisX.Title = "Time [ms]";
            crtVariables.ChartAreas[0].AxisY.Title = "Rset";
            crtVariables.ChartAreas[0].AxisY2.Title = "Ract";

            crtVariables.ChartAreas[0].AxisX.Minimum = 0;
        }

        /// <summary>
        /// Initialize the chart (async) task updater
        /// </summary>
        private void InitiazlieChartUpdater()
        {
            startTime = new TimeSpan(DateTime.Now.Ticks);
            bgWorker.DoWork += ChartUpdater_DoWork;
            bgWorker.RunWorkerAsync();

        }

        /// <summary>
        /// Initialize user interface-related components
        /// </summary>
        private void InitializeUserInterface()
        {
            pnlResourceStarted.BackColor = unknowkColor;
        }

        /// <summary>
        /// Initialize the can-related objects
        /// (like the <see cref="PeakCanResource"/> or the 
        /// various <see cref="CanChannel"/>)
        /// </summary>
        private void InitializeCanCommunication()
        {
            resource = new PeakCanResource();

            lblResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();
            resource.StatusChanged += Resource_StatusChanged;

            actualResistance = new CanChannel();
            setResistance = new CanChannel();

            resource.Channels.Add(actualResistance);
            resource.Channels.Add(setResistance);
        }

        // Handle the resource status value changed
        private void Resource_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            lblResourceStatus.Invoke(new MethodInvoker(()=>
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

            // Initialize variables
            rSet = 0.0;
            rAct = 0.0;

            // Initialize chart
            InitializeChart();

            // Initialize chart task updater
            InitiazlieChartUpdater();
        }

        // Handle initialization that can only be done
        // after the form controll has been created
        // (in particular the can communication initialization
        // because there is an Invoke method call inside)
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize can-related objects
            // This operation is performed in the Load event because
            // a call to the Invoke method is performed in the code below
            InitializeCanCommunication();
        }

        // Update the chart
        private async void ChartUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            int x = 0;

            while (continueToUpdateChart)
            {
                x = (int)(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds - startTime.TotalMilliseconds);

                // Dummy values, have to be replaced with the values read via can protocol 
                // (Rset is an asynchronous read, once when a different value is written,
                // while Ract is a periodic read - the frame is automatically sent
                // by the board in the can bus every x milliseconds)
                rSet = Math.Sin(x);
                rAct = Math.Cos(x);

                crtVariables.Invoke((MethodInvoker)delegate
                    {
                        crtVariables.Series[rSetName].Points.AddXY(x, rSet);
                        crtVariables.Series[rActName].Points.AddXY(x, rAct);

                        if(crtVariables.Series[rSetName].Points.Count > numberOfPOints)
                        {
                            crtVariables.Series[rSetName].Points.RemoveAt(0);
                            crtVariables.Series[rActName].Points.RemoveAt(0);

                            crtVariables.ChartAreas[0].AxisX.Minimum = crtVariables.Series[rSetName].Points[0].XValue;
                            crtVariables.ResetAutoValues();
                        }
                    }
                );                

                await Task.Delay(updateInterval);
            }
        }

        // Handle the start button click
        private void BtnStart_Click(object sender, EventArgs e)
        {
            resource?.Start(); // Start the resource
            resource?.EnableLog(); // And enable the log

            pnlResourceStarted.BackColor = startedColor;
        }

        // Handle the stop button click
        private void BtnStop_Click(object sender, EventArgs e)
        {
            resource?.Stop();

            pnlResourceStarted.BackColor = stoppedColor;
        }

        // Handle the read log button click
        private void BtnReadLog_Click(object sender, EventArgs e)
        {
            lbxLog.Items.Clear(); // Clear previous log first
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
    }
}
