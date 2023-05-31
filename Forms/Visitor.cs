using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Visitor : Form
    {
        private DbManager _dbManager;
        public Visitor()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Visitor_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("visitor", dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            /*String[] fields = {"id", "fullname", "age", "allergies", "contacts" };
                     String[] values = {textBox1.Text, "'"+ textBox2.Text+ "'", "'" + textBox3.Text+ "'", "'"+textBox4.Text+ "'","'"+ textBox5.Text + "'"};
                     _dbManager.Insert("visitor",fields,values );*/
            _dbManager.ExecuteSql($"insert into visitor(id, fullname, age, allergies, contacts) VALUES ({Convert.ToInt32(textBox1.Text)}, '{textBox2.Text}', {Convert.ToInt32(textBox3.Text)}, '{textBox4.Text}', '{textBox5.Text}')");

            _dbManager.SelectAll("visitor", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("visitor", "id="+textBox1.Text);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }
            }
            _dbManager.SelectAll("visitor", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("visitor", "");
            _dbManager.SelectAll("visitor", dataGridView1);
        }
    }
}