using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    public partial class Orders : Form
    {
        private DbManager _dbManager;
        
        public Orders()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("orders", dataGridView1);
            _dbManager.FillComboBoxWithData("visitor", "ID", "FullName", comboBox1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            DateTime selectedDate = dateTimePicker1.Value.Date;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");

            _dbManager.ExecuteSql($"insert into orders(ordernum, priority, duedate, totalprice, visitors) values ({Convert.ToInt32(textBox1.Text)}, " +
                                  $"{Convert.ToInt32(textBox4.Text)}, Date('{formattedDate}'), {Convert.ToDouble(textBox2.Text)}, '{selectedItem}')");
            _dbManager.SelectAll("orders", dataGridView1);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("orders", "ordernum="+textBox1.Text);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }
            }
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
            _dbManager.SelectAll("orders", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("orders", "");
            _dbManager.SelectAll("orders", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value != null)
                {
                    if (DateTime.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), out DateTime dateValue))
                    {
                        dateTimePicker1.Value = dateValue;
                    }
                }
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                
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
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            int ordernum = Convert.ToInt32(textBox1.Text);
            int priority = Convert.ToInt32(textBox4.Text);
            DateTime selectedDate = dateTimePicker1.Value.Date;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");
            double totalPrice = Convert.ToDouble(textBox2.Text);
            var visitors = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;

            _dbManager.ExecuteSql($"UPDATE orders SET OrderNum = {ordernum}, Priority = {priority}, DueDate = DATE ('{formattedDate}'), TotalPrice = {totalPrice}, Visitors = {visitors} " +
                                  $"WHERE OrderNum = {dataGridView1.Rows[rowIndex].Cells[0].Value}");
            
            _dbManager.SelectAll("orders", dataGridView1);


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                
                if (textBox1.Text == null && textBox2.Text == null && textBox4.Text == null && comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Введіть дані");
                    checkBox1.Checked = false;
                    return;
                }
                
                Dictionary<string, object> searchParameters = new Dictionary<string, object>();

                if (textBox1.Text != null && textBox1.Text.Length != 0)
                {
                    searchParameters.Add("OrderNum", Convert.ToInt32(textBox1.Text));
                }

                if (textBox2.Text != null && textBox2.Text.Length != 0)
                {
                    searchParameters.Add("TotalPrice", textBox2.Text);
                }                
                
                if (textBox4.Text.Length != 0)
                {
                    searchParameters.Add("Priority", Convert.ToInt32(textBox4.Text));
                }                
                
                if (comboBox1.SelectedItem != null)
                {
                    searchParameters.Add("Visitors", ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key);
                }
                

                DataTable resultTable = _dbManager.SearchData(searchParameters, "orders");

                dataGridView1.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = resultTable;

                dataGridView1.DataSource = bindingSource;
            }
            else
            {
                dataGridView1.DataSource = null;
                _dbManager.SelectAll("orders", dataGridView1);
            }
        }
    }
}