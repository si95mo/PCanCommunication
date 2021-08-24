using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCanCommunication
{
    public partial class MainForm : Form
    {
        private TimeSpan startTime;

        private readonly int numberOfPOints = 64;

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
            crtVariables.Series[rActName].ChartType = SeriesChartType.FastLine;
            crtVariables.Series[rActName].YAxisType = AxisType.Secondary;

            crtVariables.ChartAreas[0].AxisX.Title = "Time [ms]";
            crtVariables.ChartAreas[0].AxisY.Title = "Rset";
            crtVariables.ChartAreas[0].AxisY2.Title = "Ract";

            crtVariables.ChartAreas[0].AxisX.Minimum = 0;
        }

        public MainForm()
        {
            InitializeComponent();

            rSet = 0.0;
            rAct = 0.0;

            InitializeChart();

            startTime = new TimeSpan(DateTime.Now.Ticks);
            bgWorker.DoWork += ChartUpdater_DoWork;
            bgWorker.RunWorkerAsync();
        }

        private async void ChartUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            int x = 0;

            while (true)
            {
                rSet = new Random(Guid.NewGuid().GetHashCode()).NextDouble();
                rAct = 10 * new Random(Guid.NewGuid().GetHashCode()).NextDouble();

                x = (int)(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds - startTime.TotalMilliseconds);

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

                await Task.Delay(100);
            }
        }
    }
}
