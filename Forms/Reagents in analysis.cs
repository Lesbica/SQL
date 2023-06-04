using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    public partial class Reagents_in_analysis : Form
    {
        private DbManager _dbManager;
        
        public Reagents_in_analysis()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Reagents_in_analysis_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("reagentsinanalysis", dataGridView1);
            _dbManager.FillComboBoxWithData("reagent", "ReagCode", "NameReagent", comboBox2);
            _dbManager.FillComboBoxWithData("analysis", "AnalCode", "Price", comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            var selectedItem2 = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;
            _dbManager.ExecuteSql($"insert into reagentsinanalysis(usedcount, analcode, reagcode) values ( {Convert.ToInt32(textBox2.Text)}," +
                                  $"'{selectedItem}', '{selectedItem2}')");
            _dbManager.SelectAll("reagentsinanalysis", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            _dbManager.Delete("reagentsinanalysis", "AnalCode="+selectedItem);
            comboBox2.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            _dbManager.SelectAll("reagentsinanalysis", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("reagentsinanalysis", "");
            _dbManager.SelectAll("reagentsinanalysis", dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            var AnalCode = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            var ReagCode = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;

            _dbManager.ExecuteSql($"UPDATE reagentsinanalysis SET UsedCount = {Convert.ToInt32(textBox2.Text)}, AnalCode = {AnalCode}, ReagCode = {ReagCode} " +
                                  $"WHERE AnalCode = {dataGridView1.Rows[rowIndex].Cells[1].Value}");
            
            _dbManager.SelectAll("reagentsinanalysis", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            
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
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                
                if (comboBox1.SelectedItem == null && comboBox2.SelectedItem == null && string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Введіть дані");
                    checkBox1.Checked = false;
                    return;
                }
                
                Dictionary<string, object> searchParameters = new Dictionary<string, object>();

                if (comboBox1.SelectedItem != null)
                {
                    searchParameters.Add("AnalCode", ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key);
                }

                if (comboBox2.SelectedItem != null)
                {
                    searchParameters.Add("ReagCode", ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key);
                }
                
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    searchParameters.Add("AnalCode", Convert.ToInt32(textBox2.Text));
                }
                

                DataTable resultTable = _dbManager.SearchData(searchParameters, "reagentsinanalysis");

                dataGridView1.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = resultTable;

                dataGridView1.DataSource = bindingSource;
            }
            else
            {
                dataGridView1.DataSource = null;
                _dbManager.SelectAll("reagentsinanalysis", dataGridView1);
            }
        }
    }
}