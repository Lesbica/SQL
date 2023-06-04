using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    public partial class Analysis_Orders : Form
    {
        private DbManager _dbManager;
        
        public Analysis_Orders()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Analysis_Orders_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("analysisorders", dataGridView1);
            _dbManager.FillComboBoxWithData("orders", "OrderNum", "OrderNum", comboBox1);
            _dbManager.FillComboBoxWithData("analysis", "AnalCode", "Price", comboBox3);
            _dbManager.FillComboBoxWithData("result", "CodeResult", "ShortInfo", comboBox2);
            _dbManager.FillComboBoxWithData("labassistant", "ID", "FullName", comboBox4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value.Date;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            var selectedItem2 = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;
            var selectedItem3 = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
            var selectedItem4 = ((KeyValuePair<int, string>)comboBox4.SelectedItem).Key;

            _dbManager.ExecuteSql($"insert into analysisorders(dateofresults, ordernum, result, analcode, labassistant) values (DATE ('{formattedDate}'), " +
                                  $"'{selectedItem}', '{selectedItem2}', '{selectedItem3}', '{selectedItem4}')");
            _dbManager.SelectAll("analysisorders", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            _dbManager.Delete("analysisorders", "ordernum="+selectedItem);
            comboBox2.Text = "";
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
            _dbManager.SelectAll("analysisorders", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("analysisorders", "");
            _dbManager.SelectAll("analysisorders", dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            
            DateTime selectedDate = dateTimePicker1.Value.Date;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");
            
            var OrderNum = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            var Result = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;
            var AnalCode = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
            var LabAssistant = ((KeyValuePair<int, string>)comboBox4.SelectedItem).Key;

            _dbManager.ExecuteSql($"UPDATE analysisorders SET DateOfResults = DATE('{formattedDate}'), OrderNum = {OrderNum}, Result = {Result}, AnalCode = {AnalCode}, LabAssistant = {LabAssistant} " +
                                  $"WHERE OrderNum = {dataGridView1.Rows[rowIndex].Cells[1].Value}");
            
            _dbManager.SelectAll("analysisorders", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            KeyValuePair<int, string> desiredItem = null;
            foreach (KeyValuePair<int, string> item in comboBox1.Items)
            {
                if (item.Key == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                {
                    desiredItem = item;
                    break;
                }
            }
                
            if (desiredItem != null)
            {
                comboBox1.SelectedItem = desiredItem;
            }
            
            KeyValuePair<int, string> desiredItem2 = null;
            foreach (KeyValuePair<int, string> item in comboBox2.Items)
            {
                if (item.Key == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value))
                {
                    desiredItem2 = item;
                    break;
                }
            }
                
            if (desiredItem2 != null)
            {
                comboBox2.SelectedItem = desiredItem2;
            }
            
            KeyValuePair<int, string> desiredItem3 = null;
            foreach (KeyValuePair<int, string> item in comboBox3.Items)
            {
                if (item.Key == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value))
                {
                    desiredItem3 = item;
                    break;
                }
            }
                
            if (desiredItem3 != null)
            {
                comboBox3.SelectedItem = desiredItem3;
            }
            
            KeyValuePair<int, string> desiredItem4 = null;
            foreach (KeyValuePair<int, string> item in comboBox4.Items)
            {
                if (item.Key == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value))
                {
                    desiredItem4 = item;
                    break;
                }
            }
                
            if (desiredItem4 != null)
            {
                comboBox4.SelectedItem = desiredItem4;
            }
            
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                if (DateTime.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out DateTime dateValue))
                {
                    dateTimePicker1.Value = dateValue;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                
                if (comboBox1.SelectedItem == null && comboBox2.SelectedItem == null && comboBox3.SelectedItem == null && comboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Введіть дані");
                    checkBox1.Checked = false;
                    return;
                }
                
                Dictionary<string, object> searchParameters = new Dictionary<string, object>();

                if (comboBox1.SelectedItem != null)
                {
                    searchParameters.Add("OrderNum", ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key);
                }

                if (comboBox2.SelectedItem != null)
                {
                    searchParameters.Add("Result", ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key);
                }
                
                if (comboBox3.SelectedItem != null)
                {
                    searchParameters.Add("AnalCode", ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key);
                }
                
                if (comboBox4.SelectedItem != null)
                {
                    searchParameters.Add("LabAssistant", ((KeyValuePair<int, string>)comboBox4.SelectedItem).Key);
                }

                DataTable resultTable = _dbManager.SearchData(searchParameters, "analysisorders");

                dataGridView1.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = resultTable;

                dataGridView1.DataSource = bindingSource;
            }
            else
            {
                dataGridView1.DataSource = null;
                _dbManager.SelectAll("analysisorders", dataGridView1);
            }
        }
    }
}