using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Charts : Form
    {

        public Charts(string chartNameSeries, DataGridView dataGridView, int columnIndex)
        {

            InitializeComponent();

            //Chart name

            chart1.Series[0].LegendText = chartNameSeries;
            
            // Clear all points in series 

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            // Add all values from selected column to list

            List<string> chartList = new List<string>();
            foreach (DataGridViewRow item in dataGridView.Rows)
            {
                if (item.Cells.Count >= 2 && item.Cells[0].Value != null)
                {
                    chartList.Add(item.Cells[columnIndex].Value.ToString());
                }
            }
            
            // Convert chart list to double

            List<double> chartListToDouble = chartList.Select(x => double.Parse(x)).ToList();

            foreach (double item in chartListToDouble)
            {
                chart1.Series[0].Points.AddY(item);
            }

            chart1.ChartAreas[0].RecalculateAxesScale();

            chart1.Series[0].MarkerSize = 10;

            chartListToDouble.Sort();
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorY.AutoScroll = true;
        }

        //Close by pressing ESC key

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

    }
}
