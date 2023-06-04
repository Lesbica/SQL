using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    public partial class Reagent : Form
    {
        private DbManager _dbManager;
        
        public Reagent()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Reagent_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("reagent", dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _dbManager.ExecuteSql($"insert into reagent(reagcode, namereagent, descriptionreagent, available) values ({Convert.ToInt32(textBox1.Text)}, '{textBox2.Text}'," +
                                  $" '{textBox3.Text}' , {Convert.ToInt32(textBox4.Text)})");
            _dbManager.SelectAll("reagent", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("reagent", "reagcode="+textBox1.Text);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }
            }
            _dbManager.SelectAll("reagent", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("reagent", "");
            _dbManager.SelectAll("reagent", dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            int ReagCode = Convert.ToInt32(textBox1.Text);
            string namereagent = textBox2.Text;
            string descriptionreagent = textBox3.Text;
            int available = Convert.ToInt32(textBox4.Text);
            

            _dbManager.ExecuteSql($"UPDATE reagent SET ReagCode = {ReagCode}, NameReagent = '{namereagent}', DescriptionReagent = '{descriptionreagent}', Available = '{available}' " +
                                  $"WHERE ReagCode = {dataGridView1.Rows[rowIndex].Cells[0].Value}");
            
            _dbManager.SelectAll("reagent", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
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

                if (textBox1.Text == null && textBox2.Text == null && textBox3.Text == null && string.IsNullOrEmpty(textBox4.Text)) 
                {
                    MessageBox.Show("Введіть дані");
                    checkBox1.Checked = false;
                    return;
                }
                
                Dictionary<string, object> searchParameters = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    searchParameters.Add("ReagCode", Convert.ToInt32(textBox1.Text));
                }

                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    searchParameters.Add("NameReagent", textBox2.Text);
                }                
                
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    searchParameters.Add("DescriptionReagent", textBox3.Text);
                }
                
                if (!string.IsNullOrEmpty(textBox4.Text))
                {
                    searchParameters.Add("Available", Convert.ToInt32(textBox4.Text));
                }

                DataTable resultTable = _dbManager.SearchData(searchParameters, "reagent");

                dataGridView1.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = resultTable;

                dataGridView1.DataSource = bindingSource;
            }
            else
            {
                dataGridView1.DataSource = null;
                _dbManager.SelectAll("reagent", dataGridView1);
            }
        }
    }
}