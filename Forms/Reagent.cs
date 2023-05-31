using System;
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
    }
}