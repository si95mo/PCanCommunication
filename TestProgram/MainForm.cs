using DataStructures.VariablesDictionary;
using Hardware.Can;
using Hardware.Can.Peak.Lib;
using Instructions.Scheduler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TestProgram
{
    public partial class MainForm : Form
    {
        // Color "constants"
        private readonly Color startedColor = Color.Green;

        private readonly Color stoppedColor = Color.Red;
        private readonly Color unknowkColor = Color.DarkGray;

        // Can resource variables
        private ushort hardwareHandle = 0; // The hardware handle (changed in InitializeCanCommunication)

        private PeakCanResource resource;

        private string testPath;
        private string variablePath;
        private string folderPath;
        private string resultPath;

        private Scheduler scheduler;

        private bool testFileSelected;
        private bool testFolderSelected;
        private bool testSelected;

        private IndexedCanChannel tx, rx;

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

            BindingSource bs = new BindingSource
            {
                DataSource = hardwareNames
            };

            cbxDeviceList.DataSource = bs.DataSource;
            cbxDeviceList.SelectedIndex = cbxDeviceList.Items.Count > 0 ? 0 : -1;

            bs = new BindingSource
            {
                DataSource = new string[]
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
                }
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
            // Create the can resource
            resource = new PeakCanResource(hardwareHandle, StringToBaudRate(cbxBaudRate.SelectedItem.ToString()));

            // Update the UI and connect the event handler
            lblResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();
            resource.StatusChanged += Resource_StatusChanged;

            if (resource.Status != 0) // 0 is for no error
                lblResourceStatus.ForeColor = stoppedColor;
            else
                lblResourceStatus.ForeColor = startedColor;

            rx = new IndexedCanChannel(canId: 0x200, index: 0x0, subIndex: 0x0, resource, cmd: 0);
            rx.CanFrameChanged += Channel_CanFrameChanged;
            tx = new IndexedCanChannel(canId: 0x100, index: 0x0, subIndex: 0x0, resource, cmd: 1);

            resource.AddFilteredCanId(0x200); // Rx
            resource.AddFilteredCanId(0x100); // Tx
        }

        /// <summary>
        /// Handle the resource status changed
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The <see cref="StatusChangedEventArgs"/></param>
        private void Resource_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Update the UI component (different thread)
            lblResourceStatus.Invoke(new MethodInvoker(() =>
            {
                lblResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();

                if (lblResourceStatus.Text.CompareTo("PCAN_ERROR_OK") != 0) // PCAN_ERROR_OK is for no error
                    lblResourceStatus.ForeColor = Color.Red;
                else
                    lblResourceStatus.ForeColor = Color.Black;
            }
                )
            );
        }

        private void CbxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
            => resource?.SetBaudRate(StringToBaudRate(cbxBaudRate.SelectedItem.ToString()));

        /// <summary>
        /// Convert a textual representation of a <see cref="BaudRate"/>
        /// </summary>
        /// <param name="baudRate">The baud rate as <see cref="string"/></param>
        /// <returns>The baudrate as <see cref="BaudRate"/></returns>
        private BaudRate StringToBaudRate(string baudRate)
        {
            BaudRate convertedBaudRate;

            switch (baudRate)
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

        public MainForm()
        {
            InitializeComponent();

            testPath = "";
            folderPath = "";
            resultPath = "";

            testFileSelected = false;
            testFolderSelected = false;
            testSelected = false;

            VariableDictionary.Initialize();
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

            // Update the filtered can id
            UpdateFilteredCanId();

            resource?.Start();
        }

        private void BtnSelectTest_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                testPath = Path.Combine(folderDialog.SelectedPath, "test.csv");
                lblTestSelected.Text = testPath;

                testFileSelected = true;
                testSelected = testFileSelected && testFolderSelected;

                scheduler = new Scheduler(testPath);

                variablePath = Path.Combine(folderDialog.SelectedPath, "variables.csv");
                VariableFileHandler.ReadTest(variablePath);

                foreach (IVariable v in VariableDictionary.Variables.Values)
                    (v as DoubleVariable).ValueChanged += Variable_ValueChanged;
            }
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                folderPath = folderDialog.SelectedPath;
                lblFolderSelected.Text = folderPath;

                testFolderSelected = true;
                testSelected = testFileSelected && testFolderSelected;

                resultPath = Path.Combine(folderPath, $"{DateTime.Now:yyyyMMddHHmmss}_result.csv");
            }
        }

        private async void BtnStartTest_Click(object sender, EventArgs e)
        {
            if (testSelected)
            {
                await scheduler?.ExecuteAll(resultPath, tx);
                MessageBox.Show("Test completed!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No test or result folder selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnStopTest_Click(object sender, EventArgs e)
        {
            scheduler.StopAll();
        }

        private void Channel_CanFrameChanged(object sender, CanFrameChangedEventArgs e)
        {
            VariableDictionary.Variables
                .Where(x => x.Value.Index == rx.Index && x.Value.SubIndex == rx.SubIndex)
                .Select(x => x.Value.ValueAsObject = BitConverter.ToSingle(rx.CanFrame.Data, 4));
        }

        private void Variable_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            DoubleVariable variable = (DoubleVariable)sender;

            tx.Index = variable.Index;
            tx.SubIndex = variable.SubIndex;
            tx.Data = BitConverter.GetBytes((float)variable.Value);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Start the resource
            resource?.Start();
            // Enable the log
            resource?.EnableLog();

            // Update UI
            btnStart.FlatAppearance.BorderColor = startedColor;
            btnStop.FlatAppearance.BorderColor = Color.Black;
            pnlResourceStarted.BackColor = startedColor;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            // Stop the resource.
            // Doesn't disable the log in order to not delete unseen log entries
            // (multiple invokes of EnableLog will delete the log)
            resource?.Stop();

            // Update UI
            btnStart.FlatAppearance.BorderColor = Color.Black;
            btnStop.FlatAppearance.BorderColor = stoppedColor;
            pnlResourceStarted.BackColor = stoppedColor;
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

        /// <summary>
        /// Update the UI component with the updated
        /// filtered can id
        /// </summary>
        private void UpdateFilteredCanId()
        {
            Dictionary<int, bool> ids = resource?.FilteredCanId;

            lbxFilteredCanId.Items.Clear();
            foreach (KeyValuePair<int, bool> id in ids)
            {
                if (id.Value)
                    lbxFilteredCanId.Items.Add($"0x{id.Key:X2}");
            }
        }

        private void BtnAddFiltered_Click(object sender, EventArgs e)
        {
            resource?.AddFilteredCanId((int)nudFilter.Value);
            UpdateFilteredCanId();
        }

        private void BtnRemoveFilter_Click(object sender, EventArgs e)
        {
            resource?.RemoveFilteredCanId((int)nudFilter.Value);
            UpdateFilteredCanId();
        }

        private void CbxDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cbxDeviceList.Text;
            str = str.Substring(str.IndexOf('(') + 1, 3);
            str = str.Replace('h', ' ').Trim(' ');

            hardwareHandle = Convert.ToUInt16(str, 16);
        }

        private void BtnReadLog_Click(object sender, EventArgs e)
        {
            txbLog.Text = resource?.ReadLog();
        }
    }
}