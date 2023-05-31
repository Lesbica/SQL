using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Analysis_Type : Form
    {
        private DbManager _dbManager;
        
        public Analysis_Type()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Analysis_Type_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("analysistype", dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _dbManager.ExecuteSql($"insert into analysistype(codeanaltype, nameanaltype, descriptionanaltype) values ({Convert.ToInt32(textBox1.Text)}, '{textBox2.Text}', '{textBox3.Text}')");
            _dbManager.SelectAll("analysistype", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("analysistype", "ID="+textBox1.Text);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }
            }
            _dbManager.SelectAll("analysistype", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("analysistype", "");
            _dbManager.SelectAll("analysistype", dataGridView1);
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

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            int CodeAnalType = Convert.ToInt32(textBox1.Text);
            string NameAnalType = textBox2.Text;
            string DescriptionAnalType = textBox3.Text;
            

            _dbManager.ExecuteSql($"UPDATE analysistype SET CodeAnalType = {CodeAnalType}, NameAnalType = '{NameAnalType}', DescriptionAnalType = '{DescriptionAnalType}' " +
                                  $"WHERE CodeAnalType = {dataGridView1.Rows[rowIndex].Cells[0].Value}");
            
            _dbManager.SelectAll("analysistype", dataGridView1);
        }
    }
}