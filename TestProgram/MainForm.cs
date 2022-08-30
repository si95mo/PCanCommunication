using DataStructures.VariablesDictionary;
using Hardware.Can;
using Hardware.Can.Peak.Lib;
using Instructions.Scheduler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private IndexedCanChannel tx, rx;

        // File-related variables
        private string testPath;
        private string batchFilePath;

        private string variablePath;
        private string folderPath;
        private string resultPath;

        // Scheduler-related variables
        private Scheduler scheduler;

        private int totalSteps;
        private bool doUpdateSteps; // Used in the updating step task

        // Test selected logic-related variables
        private bool testFileSelected;

        private bool testFolderSelected;
        private bool testSelected;

        /// <summary>
        /// Initialize user interface-related components
        /// </summary>
        private void InitializeUserInterface()
        {
            // DataGridView initialization
            dgvVariables.ReadOnly = true;
            dgvVariables.RowsDefaultCellStyle.BackColor = Color.FromArgb(0x58, 0x58, 0x54);
            dgvVariables.AlternatingRowsDefaultCellStyle.BackColor = ControlPaint.Light(Color.FromArgb(0x58, 0x58, 0x54));
            dgvVariables.ForeColor = Color.White;

            // Set the resource started led to a default color
            pnlResourceStarted.BackColor = unknowkColor;
            ledResourceStatus.BackColor = unknowkColor;
            // Set the log button border color to stopped
            btnReadLog.FlatAppearance.BorderColor = startedColor;

            // Get available hardware
            List<string> hardwareNames = PeakCanResource.GetAvailableHardware();
            BindingSource bs = new BindingSource
            {
                DataSource = hardwareNames
            };
            cbxDeviceList.DataSource = bs.DataSource;
            cbxDeviceList.SelectedIndex = cbxDeviceList.Items.Count > 0 ? 0 : -1;

            // Available baud-rates
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
            cbxBaudRate.SelectedIndex = 0; // 1000 kbit/s
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
            if (resource == null)
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
                        lblCanResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();

                        if (lblResourceStatus.Text.CompareTo("PCAN_ERROR_OK") != 0) // PCAN_ERROR_OK is for no error
                        {
                            lblResourceStatus.ForeColor = Color.Red;
                            lblCanResourceStatus.ForeColor = Color.Red;
                        }
                        else
                        {
                            lblResourceStatus.ForeColor = Color.Black;
                            lblCanResourceStatus.ForeColor = Color.Black;
                        }
                    }
                )
            );
        }

        /// <summary>
        /// Update the can resource baud rate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Create a new instance of <see cref="MainForm"/>
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Variables initialization
            testPath = string.Empty;
            batchFilePath = string.Empty;
            folderPath = string.Empty;
            resultPath = string.Empty;

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

            // Form position
            StartPosition = FormStartPosition.Manual;
            Location = new Point(8, 8);
        }

        // Select the test file
        private void BtnSelectTest_Click(object sender, EventArgs e)
        {
            // The can resource has to be started first
            if (resource == null)
                MessageBox.Show("CAN resource not started!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Select the test programs folder";
                ledTestLoaded.BackColor = stoppedColor;

                if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    testPath = Path.Combine(folderDialog.SelectedPath, "main.tp");
                    lblTestSelected.Text = testPath;

                    testFileSelected = true;
                    ledTestLoaded.BackColor = startedColor;
                    testSelected = testFileSelected && testFolderSelected;

                    FolderBrowserDialog batchFolderDialog = new FolderBrowserDialog();
                    batchFolderDialog.Description = "Select the batch file folder";

                    if(batchFolderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(batchFolderDialog.SelectedPath))
                        batchFilePath = batchFolderDialog.SelectedPath;

                    // Initialize the scheduler (the test file read is done inside)
                    scheduler = new Scheduler(testPath, batchFilePath);
                    totalSteps = scheduler.Instructions.Count;

                    // Update the can id or create the can channel if not already done (tx)
                    if (tx != null)
                        tx.CanId = TestProgramManager.TxCanId;
                    else
                        tx = new IndexedCanChannel(canId: TestProgramManager.TxCanId, index: 0x0, subIndex: 0x0, resource, cmd: 1);

                    // Update the can id or create the can channel if not already done (rx)
                    if (rx != null)
                        rx.CanId = TestProgramManager.RxCanId;
                    else
                        rx = new IndexedCanChannel(canId: TestProgramManager.RxCanId, index: 0x0, subIndex: 0x0, resource, cmd: 0);

                    resource.AddFilteredCanId(rx.CanId); // Rx
                    resource.AddFilteredCanId(tx.CanId); // Tx

                    // Read the variable file
                    variablePath = Path.Combine(folderDialog.SelectedPath, "variables.csv");
                    VariableDictionary.Variables.Clear();
                    VariableFileHandler.ReadTest(variablePath, '\t');

                    foreach (IVariable v in VariableDictionary.Variables.Values)
                        (v as DoubleVariable).ValueChanged += Variable_ValueChanged;

                    UpdateDataGridItems();
                }
            }
        }

        /// <summary>
        /// Update the DataGridView with all the variables found in the relative file
        /// </summary>
        private void UpdateDataGridItems()
        {
            BindingSource bs = new BindingSource
            {
                DataSource = VariableDictionary.Variables.Values.ToList()
            };
            dgvVariables.Invoke(new MethodInvoker(() => dgvVariables.DataSource = bs.DataSource));
        }

        /// <summary>
        /// Select the result folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Select the test results folder";
            ledResultFolderSelected.BackColor = stoppedColor;

            if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                folderPath = folderDialog.SelectedPath;
                lblFolderSelected.Text = folderPath;

                testFolderSelected = true;
                ledResultFolderSelected.BackColor = startedColor;
                testSelected = testFileSelected && testFolderSelected;
            }
        }

        /// <summary>
        /// Update the actual steps done by the scheduler
        /// </summary>
        /// <returns>The (async) <see cref="Task"/></returns>
        private async Task UpdateSteps()
        {
            // Log header
            txbTestLog.Invoke(new MethodInvoker(() =>
                    txbTestLog.Text =
                        $"Name\tID\tOrder\tVariable involved\tValue\tCondition to verify\tStart time\tStop time\tResult{Environment.NewLine}"
                            .Replace("\t", "  ")
                )
            );

            // Steps done and instruction executed
            string str = "";
            while (doUpdateSteps)
            {
                str = $"{totalSteps - scheduler.Instructions.Count}/{totalSteps}";
                lblSchedulerStepDone.Invoke(new MethodInvoker(() => lblSchedulerStepDone.Text = str));
                lblBasicStepNumber.Invoke(new MethodInvoker(() => lblBasicStepNumber.Text = str));

                lblInstructionDescription.Invoke(new MethodInvoker(() => lblInstructionDescription.Text = scheduler.ActualInstructionDescription));
                lblBasicInstruction.Invoke(new MethodInvoker(() => lblBasicInstruction.Text = scheduler.ActualInstructionDescription));

                await Task.Delay(10);
            }

            txbTestLog.Invoke(new MethodInvoker(() =>
                    {
                        txbTestLog.Text += Environment.NewLine;
                        txbTestLog.SelectionStart = txbTestLog.Text.Length;
                        txbTestLog.SelectionLength = 0;
                        txbTestLog.ScrollToCaret();
                    }
                )
            );
        }

        /// <summary>
        /// Update the log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scheduler_InstructionLogChanged(object sender, InstructionLogChangedEventArgs e)
        {
            txbTestLog.Invoke(new MethodInvoker(() =>
                    {
                        txbTestLog.Text += scheduler.InstructionLog + Environment.NewLine;
                        txbTestLog.SelectionStart = txbTestLog.Text.Length;
                        txbTestLog.SelectionLength = 0;
                        txbTestLog.ScrollToCaret();
                    }
                )
            );
        }

        /// <summary>
        /// Start the test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnStartTest_Click(object sender, EventArgs e)
        {
            if (!CheckTextBoxes())
                MessageBox.Show("Enter the required information first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (resource == null)
                    MessageBox.Show("CAN resource not started!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    if (resource.Status != 0)
                        MessageBox.Show("CAN resource not working!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        if (testSelected)
                        {
                            // Preliminary info
                            TestProgramManager.UserName = txbUser.Text;
                            TestProgramManager.ProductionSite = txbOperatingSite.Text;
                            TestProgramManager.BatchNumber = txbBatch.Text;

                            doUpdateSteps = true;

                            if (scheduler != null)
                                scheduler.InstructionLogChanged += Scheduler_InstructionLogChanged;

                            string[] files = Directory.GetFiles(folderPath);
                            DateTime now = DateTime.Now;
                            TestProgramManager.SerialIndex = files.Length;

                            // UI update
                            lblTestResult.Invoke(new MethodInvoker(() =>
                                    {
                                        Color textColor = Color.Black;
                                        lblTestResult.ForeColor = textColor;

                                        lblTestResult.Text = "In progress...";
                                    }
                                )
                            );
                            lblBasicTestResult.Invoke(new MethodInvoker(() =>
                                    {
                                        Color textColor = Color.Black;
                                        lblBasicTestResult.ForeColor = textColor;

                                        lblBasicTestResult.Text = "In progress...";

                                        btnStartTest.Enabled = false;
                                        btnStartTestProgram.Enabled = false;
                                    }
                                )
                            );

                            resultPath = Path.Combine(
                                folderPath,
                                $"{SerialNumbers.SerialNumbers.CreateNew(txbOperatingSite.Text, files.Length)}_{now:yyyyMMdd}_{now:HHmmss}.csv"
                            );
                            await Task.WhenAny(scheduler?.ExecuteAll(resultPath, resource: resource, tx: tx, rx: rx), UpdateSteps()); // Wait for task to finish

                            // UI update
                            lblTestResult.Invoke(new MethodInvoker(() =>
                                    {
                                        Color textColor = scheduler.TestResult ? Color.Green : Color.Red;
                                        lblTestResult.ForeColor = textColor;

                                        lblTestResult.Text = scheduler.TestResult ? "Succeeded" : "Failed";
                                    }
                                )
                            );
                            lblBasicTestResult.Invoke(new MethodInvoker(() =>
                                    {
                                        Color textColor = scheduler.TestResult ? Color.Green : Color.Red;
                                        lblBasicTestResult.ForeColor = textColor;

                                        lblBasicTestResult.Text = scheduler.TestResult ? "Succeeded" : "Failed";

                                        btnStartTest.Enabled = true;
                                        btnStartTestProgram.Enabled = true;
                                    }
                                )
                            );

                            await Task.Delay(100);
                            doUpdateSteps = false;

                            if (scheduler != null)
                                scheduler.InstructionLogChanged -= Scheduler_InstructionLogChanged;

                            scheduler = new Scheduler(testPath, batchFilePath);
                            totalSteps = scheduler.Instructions.Count;
                        }
                        else
                            MessageBox.Show("No test or result folder selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// Stop the test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStopTest_Click(object sender, EventArgs e)
        {
            scheduler?.Stop();
        }

        /// <summary>
        /// Update the variables on RX value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_CanFrameChanged(object sender, CanFrameChangedEventArgs e)
        {
            VariableDictionary.Variables
                .Where(x => x.Value.Index == rx.Index && x.Value.SubIndex == rx.SubIndex)
                .Select(x => x.Value.ValueAsObject = BitConverter.ToSingle(rx.CanFrame.Data, 4)
            );
        }

        /// <summary>
        /// Update the datagrid view on variable value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Variable_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateDataGridItems();
        }

        /// <summary>
        /// Start the can resource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Initialize can-related objects
            // This operation is performed in the Load event because
            // a call to the Invoke method is performed in the code below
            InitializeCanCommunication();

            // Update the filtered can id
            UpdateFilteredCanId();

            resource?.Start();
            resource?.EnableLog();

            // Update UI
            btnStartResource.FlatAppearance.BorderColor = startedColor;
            btnStopResource.FlatAppearance.BorderColor = Color.Black;

            ledResourceStatus.BackColor = startedColor;
            pnlResourceStarted.BackColor = startedColor;
        }

        /// <summary>
        /// Stop the can resource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            // Stop the resource.
            // Doesn't disable the log in order to not delete unseen log entries
            // (multiple invokes of EnableLog will delete the log)
            resource?.Stop();

            // Update UI
            btnStartResource.FlatAppearance.BorderColor = Color.Black;
            btnStopResource.FlatAppearance.BorderColor = stoppedColor;

            ledResourceStatus.BackColor = stoppedColor;
            pnlResourceStarted.BackColor = stoppedColor;
        }

        /// <summary>
        /// Unable/disable the log from the CAN bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Add a can id to the filtered list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddFiltered_Click(object sender, EventArgs e)
        {
            resource?.AddFilteredCanId((int)nudFilter.Value);
            UpdateFilteredCanId();
        }

        /// <summary>
        /// Remove a can id from the filtered list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemoveFilter_Click(object sender, EventArgs e)
        {
            resource?.RemoveFilteredCanId((int)nudFilter.Value);
            UpdateFilteredCanId();
        }

        /// <summary>
        /// Update the hardware handle of the can resource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cbxDeviceList.Text;
            str = str.Substring(str.IndexOf('(') + 1, 3);
            str = str.Replace('h', ' ').Trim(' ');

            hardwareHandle = Convert.ToUInt16(str, 16);
        }

        /// <summary>
        /// Stop the scheduler (if running) when the form is clising
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            resource?.Stop();
        }

        /// <summary>
        /// Basic panel can resource start (different from the advanced version!)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartCanResource_Click(object sender, EventArgs e)
        {
            hardwareHandle = 81; // PCan usb defualt hw handle

            // Create the can resource
            if (resource == null)
                resource = new PeakCanResource(hardwareHandle, BaudRate.K1000);

            // Update the UI and connect the event handler
            lblCanResourceStatus.Text = ((TPCANStatus)resource.Status).ToString();
            resource.StatusChanged += Resource_StatusChanged;

            if (resource.Status != 0) // 0 is for no error
                lblCanResourceStatus.ForeColor = stoppedColor;
            else
                lblCanResourceStatus.ForeColor = startedColor;

            rx = new IndexedCanChannel(canId: 0x200, index: 0x0, subIndex: 0x0, resource, cmd: 0);
            rx.CanFrameChanged += Channel_CanFrameChanged;
            tx = new IndexedCanChannel(canId: 0x100, index: 0x0, subIndex: 0x0, resource, cmd: 1);

            resource.AddFilteredCanId(0x200); // Rx
            resource.AddFilteredCanId(0x100); // Tx

            resource.Start();

            ledResourceStatus.BackColor = startedColor;
            pnlResourceStarted.BackColor = startedColor;
        }

        /// <summary>
        /// Basic panel can resource stop (different from the advanced version!)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStopCanResource_Click(object sender, EventArgs e)
        {
            // Stop the resource.
            resource?.Stop();

            // Update UI
            ledResourceStatus.BackColor = stoppedColor;
            pnlResourceStarted.BackColor = stoppedColor;
        }

        /// <summary>
        /// Check the file integrity (MD5 must be correct)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheckFileIntegrity_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.Title = "Select result file to test";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string fileContent = reader.ReadToEnd();

                        int index = fileContent.IndexOf("User");
                        string subText = fileContent.Substring(index);

                        string oldMd5 = fileContent.Substring(0, index).Replace(Environment.NewLine, "");
                        string newMd5 = Cryptography.MD5.CreateNew(subText);

                        if (oldMd5.CompareTo(newMd5) == 0)
                            MessageBox.Show("The file is intact!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(
                                $"The file has been corrupted! {Environment.NewLine}  - Saved MD5: {oldMd5}{Environment.NewLine}  - Actual MD5: {newMd5}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                    }
                }
            }
        }

        /// <summary>
        /// Check whether the form text boxes are valorized (no empty string)
        /// </summary>
        /// <returns><see langword="true"/> if all is ok, <see langword="false"/> otherwise</returns>
        private bool CheckTextBoxes()
        {
            bool check = true;

            if (txbUser.Text.CompareTo("") == 0)
            {
                check = false;
                txbUser.Focus();
            }
            else
            {
                if (txbOperatingSite.Text.CompareTo("") == 0)
                {
                    check = false;
                    txbOperatingSite.Focus();
                }
                else
                {
                    if (txbBatch.Text.CompareTo("") == 0)
                    {
                        check = false;
                        txbBatch.Focus();
                    }
                }
            }

            return check;
        }
    }
}