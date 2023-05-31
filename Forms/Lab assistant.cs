using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Lab_assistant : Form
    {
        private DbManager _dbManager;
        
        public Lab_assistant()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Lab_assistant_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("labassistant", dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _dbManager.ExecuteSql($"insert into labassistant(id, fullname, specialization) values ({Convert.ToInt32(textBox1.Text)}, '{textBox2.Text}', '{textBox3.Text}')");
            _dbManager.SelectAll("labassistant", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("labassistant", "id="+textBox1.Text);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }
            }
            _dbManager.SelectAll("labassistant", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("labassistant", "");
            _dbManager.SelectAll("labassistant", dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            int ID = Convert.ToInt32(textBox1.Text);
            string FullName = textBox2.Text;
            string Specialization = textBox3.Text;
            

            _dbManager.ExecuteSql($"UPDATE labassistant SET ID = {ID}, FullName = '{FullName}', Specialization = '{Specialization}' " +
                                  $"WHERE ID = {dataGridView1.Rows[rowIndex].Cells[0].Value}");
            
            _dbManager.SelectAll("labassistant", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}