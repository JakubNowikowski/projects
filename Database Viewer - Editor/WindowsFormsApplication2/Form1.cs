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
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class MainWindow : Form
    {
        MySqlConnection connection = new MySqlConnection();
        Loaddb load = new Loaddb();
        DateTime MyDate;
        DateTime odMyDate;
        DateTime doMyDate;

        public MainWindow()
        {
            InitializeComponent();

            establishSQLConnection();

            string commandString = "SELECT * FROM BipolarTransistorsTests;";
       
            // DATE TIME PICKERS

            TimePicker.Format = DateTimePickerFormat.Time;
            TimePicker.ShowUpDown = true;
            
            this.DateTimePicker.Value = DateTime.Now;
            this.TimePicker.Value = DateTime.Now;

            this.doDateTimePicker.Value = DateTime.Now;
            this.doTimePicker.Value = DateTime.Now;

            MyDate = DateTimePicker.Value.Date + TimePicker.Value.TimeOfDay;
            
            odTimePicker.Format = DateTimePickerFormat.Time;
            odTimePicker.ShowUpDown = true;

            doTimePicker.Format = DateTimePickerFormat.Time;
            doTimePicker.ShowUpDown = true;

            odMyDate = odDateTimePicker.Value.Date + odTimePicker.Value.TimeOfDay;
            doMyDate = doDateTimePicker.Value.Date + doTimePicker.Value.TimeOfDay;

            // Add data to DataGridView

            try
            {
                load.AddDataToGridView(commandString, dataGridView, connection);
                iloscPomiarowLabel.Text = dataGridView.Rows.Count.ToString();

            }
            catch
            {
                this.Close();
            }
        }

        private void establishSQLConnection()
        {
            try
            {
                // Connect to mysql database

                connection.ConnectionString =
                "Data Source = sql11.freesqldatabase.com;" +
                "Initial Catalog = sql11221372;" +
                "User id = sql11221372;" +
                "Password = buDsB3Sb8S;" +
                "Convert Zero Datetime=True;" +
                "charset=utf8;";
                
                connection.Open();

            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show(sqlEx.ToString(), "Communicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        // Add row
             
        private void AddRowButton_Click(object sender, EventArgs e)
        {
            string commandString = "INSERT INTO BipolarTransistorsTests (TYPE, Uceomax, Icmax, Uebomax, Ibmax, Pstrmax, DATE_TIME)"
            + " VALUES (?type, ?Uceomax, ?Icmax, ?Uebomax, ?Ibmax, ?Pstrmax, ?DATE_TIME);";
            
            MyDate = DateTimePicker.Value.Date + TimePicker.Value.TimeOfDay;

            MySqlCommand comm = new MySqlCommand(commandString, connection);


            comm.Parameters.Add("?TYPE", typeTextbox.Text);
            comm.Parameters.Add("?Uceomax", uceomaxTextbox.Text);
            comm.Parameters.Add("?Icmax", icmaxTextbox.Text);
            comm.Parameters.Add("?Uebomax", uebomaxTextbox.Text);
            comm.Parameters.Add("?Ibmax", ibmaxTextbox.Text);
            comm.Parameters.Add("?Pstrmax", pstrmaxTextbox.Text);
            comm.Parameters.Add("?DATE_TIME", MyDate);
            
            try
            {
                comm.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Zły format", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            load.AddDataToGridView("SELECT * FROM BipolarTransistorsTests", dataGridView, connection);
            iloscPomiarowLabel.Text = (dataGridView.RowCount).ToString();
            load.CreateChart(dataGridView, uceomaxChart, 2);
            load.CreateChart(dataGridView, icmaxChart, 3);
            load.CreateChart(dataGridView, uebomaxChart, 4);
            load.CreateChart(dataGridView, ibmaxChart, 5);

        }
        
        // DataGridView content changed
        
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int selectedCellCount = dataGridView.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount >= 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < selectedCellCount; i++)
                {
                    int colIndex = dataGridView.CurrentCell.ColumnIndex;
                    int rowIndex = dataGridView.CurrentCell.RowIndex;

                    string columnName = dataGridView.Columns[colIndex].Name;
                    string idName = dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[0].Value.ToString();

                    try
                    {
                        sb.Append(dataGridView.SelectedCells[i].Value.ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Wypełnij komórkę");
                    }

                    string commandString = "UPDATE BipolarTransistorsTests SET ";
                    commandString += columnName;
                    commandString += "= ?value WHERE ID= ";
                    commandString += idName + ";";
     
                    MySqlCommand comm = new MySqlCommand(commandString, connection);

                    comm.Parameters.Add("?value", sb.ToString());

                    try
                    {
                        comm.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Błąd", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (typeSearchTextbox.Text == "")
                    
                        load.AddDataToGridView("SELECT * FROM BipolarTransistorsTests", dataGridView, connection);
                     
                    else
                        load.AddDataToGridView("SELECT * FROM BipolarTransistorsTests WHERE TYPE=('" + typeSearchTextbox.Text + "')", dataGridView, connection);
                    
                    load.CreateChart(dataGridView, uceomaxChart, 2);
                    load.CreateChart(dataGridView, icmaxChart, 3);
                    load.CreateChart(dataGridView, uebomaxChart, 4);
                    load.CreateChart(dataGridView, ibmaxChart, 5);
                    iloscPomiarowLabel.Text = (dataGridView.RowCount).ToString();

                }

            }

            
        }

        // Delete row

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            int selectedCellCount = dataGridView.GetCellCount(DataGridViewElementStates.Selected);

            DialogResult dialogResult = MessageBox.Show("Sprawdź czy zaznaczyłeś właściwy wiersz. \nCzy na pewno chcesz go usunąć ?", "Uwaga", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (selectedCellCount >= 0)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int rowIndex = dataGridView.CurrentCell.RowIndex;

                        string idName = dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[0].Value.ToString();

                        try
                        {
                            sb.Append(dataGridView.SelectedCells[i].Value.ToString());
                        }
                        catch
                        {
                            MessageBox.Show("Błąd", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        string commandString = "DELETE FROM BipolarTransistorsTests WHERE ";
                        commandString += "ID= ";
                        commandString += idName;

                        MySqlCommand comm = new MySqlCommand(commandString, connection);

                        try
                        {
                            comm.ExecuteNonQuery();
                        }
                        catch (MySqlException sqlEx)
                        {
                            MessageBox.Show(sqlEx.ToString(), "Communicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }

                    }

                    if (typeSearchTextbox.Text == "")
                        load.AddDataToGridView("SELECT * FROM BipolarTransistorsTests", dataGridView, connection);
                    else
                        load.AddDataToGridView("SELECT * FROM BipolarTransistorsTests WHERE TYPE=('" + typeSearchTextbox.Text + "')", dataGridView, connection);

                    load.CreateChart(dataGridView, uceomaxChart, 2);
                    load.CreateChart(dataGridView, icmaxChart, 3);
                    load.CreateChart(dataGridView, uebomaxChart, 4);
                    load.CreateChart(dataGridView, ibmaxChart, 5);
                    iloscPomiarowLabel.Text = (dataGridView.RowCount).ToString();

                    sb.Clear();
                }
            }
        }

        // Clear all textboxes

        private void clearButton_Click(object sender, EventArgs e)
        {
            typeTextbox.Clear(); uceomaxTextbox.Clear(); icmaxTextbox.Clear();
            uebomaxTextbox.Clear(); ibmaxTextbox.Clear(); pstrmaxTextbox.Clear(); inuseTextbox.Clear();
        }

        // Search by type

        private void typeSearchTextbox_TextChanged(object sender, EventArgs e)
        {

            if (typeSearchTextbox.Text == "")
                load.AddDataToGridView("SELECT * FROM BipolarTransistorsTests", dataGridView, connection);
            else
                load.AddDataToGridView("SELECT * FROM BipolarTransistorsTests WHERE TYPE=('" + typeSearchTextbox.Text + "')", dataGridView, connection);

            load.CreateChart(dataGridView, uceomaxChart, 2);
            load.CreateChart(dataGridView, icmaxChart, 3);
            load.CreateChart(dataGridView, uebomaxChart, 4);
            load.CreateChart(dataGridView, ibmaxChart, 5);
            iloscPomiarowLabel.Text = (dataGridView.RowCount).ToString();
        }

        // Data time changed

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            odMyDate = odDateTimePicker.Value.Date + odTimePicker.Value.TimeOfDay;
            doMyDate = doDateTimePicker.Value.Date + doTimePicker.Value.TimeOfDay;

            string startDate = Convert.ToDateTime(odMyDate).ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = Convert.ToDateTime(doMyDate).ToString("yyyy-MM-dd HH:mm:ss");

            string commandString = "SELECT * FROM BipolarTransistorsTests WHERE DATE_TIME BETWEEN '" + startDate + "' AND '" + endDate + "';";
            
            load.AddDataToGridView(commandString, dataGridView, connection);
            iloscPomiarowLabel.Text = (dataGridView.RowCount).ToString();
            
            load.CreateChart(dataGridView, uceomaxChart, 2);
            load.CreateChart(dataGridView, icmaxChart, 3);
            load.CreateChart(dataGridView, uebomaxChart, 4);
            load.CreateChart(dataGridView, ibmaxChart, 5);

        }

        // Enable adding row

        private void addCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (addCheckBox.Checked == true)
            {
                addCheckBox.Text = "Włączone";

                typeGroupBox.Enabled = true; uceomaxGroupBox.Enabled = true; icmaxGroupBox.Enabled = true; uebomaxGroupBox.Enabled = true;
                ibmaxGroupBox.Enabled = true; pstrmaxGroupBox.Enabled = true; date_timeGroupBox.Enabled = true; inuseGroupBox.Enabled = true;
                addRowButton.Enabled = true; deleteRowButton.Enabled = true; clearButton.Enabled = true;
            }
            else
            {
                addCheckBox.Text = "Wyłączone";

                typeGroupBox.Enabled = false; uceomaxGroupBox.Enabled = false; icmaxGroupBox.Enabled = false;
                uebomaxGroupBox.Enabled = false; ibmaxGroupBox.Enabled = false; pstrmaxGroupBox.Enabled = false;
                date_timeGroupBox.Enabled = false; inuseGroupBox.Enabled = false; addRowButton.Enabled = false;
                deleteRowButton.Enabled = false; clearButton.Enabled = false;
            }
        }

        // DataGridView enable editing

        private void edytowanieChecBox_CheckedChanged(object sender, EventArgs e)
        {
            if (edytowanieChecBox.Checked == true)
            {
                dataGridView.ReadOnly = false;
                edytowanieChecBox.Text = "Włączone";
                dataGridView.GridColor = Color.Red;
            }
            else
            {
                dataGridView.ReadOnly = true;
                edytowanieChecBox.Text = "Wyłączone";
                dataGridView.GridColor = Color.DarkGray;
            }
        }

        // Prevent from typing charts in float textboxes

        private void uceomaxTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            load.OnlyFloatNumbers(sender, e);
        }

        private void icmaxTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            load.OnlyFloatNumbers(sender, e);
        }

        private void uebomaxTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            load.OnlyFloatNumbers(sender, e);
        }

        private void ibmaxTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            load.OnlyFloatNumbers(sender, e);
        }

        private void pstrmaxTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            load.OnlyFloatNumbers(sender, e);
        }

        private void uceomaxChart_Click(object sender, EventArgs e)
        {
            Charts chart = new Charts(uceomaxChart.Series[0].Name, dataGridView, 2);

            chart.Show();
        }

        private void icmaxChart_Click(object sender, EventArgs e)
        {
            Charts chart = new Charts(icmaxChart.Series[0].Name, dataGridView, 3);

            chart.Show();
        }

        private void uebomaxChart_Click(object sender, EventArgs e)
        {
            Charts chart = new Charts(uebomaxChart.Series[0].Name, dataGridView, 4);

            chart.Show();
        }

        private void ibmaxChart_Click(object sender, EventArgs e)
        {
            Charts chart = new Charts(ibmaxChart.Series[0].Name, dataGridView, 5);

            chart.Show();
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


        // Close connection after closing window

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
            Application.Exit();
        }



    }
}