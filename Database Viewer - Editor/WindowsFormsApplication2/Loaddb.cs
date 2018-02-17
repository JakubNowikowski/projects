using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.IO;

namespace WindowsFormsApplication2
{
    class Loaddb
    {

        public void OnlyFloatNumbers(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public void AddDataToGridView(string cmd, DataGridView dataGridView, MySqlConnection con)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(cmd, con);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    dataGridView.Rows.Clear();

                    while (reader.Read())
                    {

                        dataGridView.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7]);
                    }
                }

            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show(sqlEx.ToString(), "Communicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        
        public void CreateChart(DataGridView dataGridView, Chart chartName, int columnIndex)
        {
            foreach (var series in chartName.Series)
            {
                series.Points.Clear();
            }
            
            List<string> chartList = new List<string>();
            foreach (DataGridViewRow item in dataGridView.Rows)
            {
                if (item.Cells.Count >= 2 && item.Cells[columnIndex].Value != null)
                {
                    chartList.Add(item.Cells[columnIndex].Value.ToString());
                }
            }

            List<double> chartListToDouble = chartList.Select(x => double.Parse(x)).ToList();

            foreach (double item in chartListToDouble)
            {
                chartName.Series[0].Points.AddY(item);
            }

            chartListToDouble.Sort();
        }
        
    }
}
