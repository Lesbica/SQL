using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SQL
{
    public partial class Conect : Form
    {
        private DbManager _dbManager;
        public Conect()
        {
            InitializeComponent();
            _dbManager = new DbManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbManager.deleteInstance();
            string conStr="";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = File.OpenText(openFileDialog1.FileName))
                {
                    byte[] bytes = File.ReadAllBytes(openFileDialog1.FileName);
                    conStr = System.Text.Encoding.UTF8.GetString(bytes);
                }
            }
            _dbManager = DbManager.getInstance(conStr);
            if (_dbManager.CheckConnection())
            {
                _dbManager.SelectAll("INFORMATION_SCHEMA.TABLES", dataGridView1);
                if (_dbManager.TableExists("Prikhodchenko"))
                {
                    BTCreate.Enabled=false;
                    BTDrop_Table.Enabled = true;
                    button2.Enabled = true;
                    CBSwitcher.Enabled = true;
                }
                else
                {
                    BTCreate.Enabled=true;
                }
                CBFilter.Enabled=true;
            }
            else MessageBox.Show("Failed connection");
            // try
            // {
            //     //@"Server= jwkrush.com.ua;Database=kkte_nau;Uid=kkte_nau;Pwd=KkTe#NaU;"
            //     _dbManager.ConnectTo($@"Server={textBox1.Text};Database={textBox2.Text};Uid={textBox3.Text};Pwd={textBox4.Text};");
            //     /*DataTable resultTable =  _dbManager.Select(@"SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'");
            //     dataGridView1.DataSource = resultTable;*/
            //    _dbManager.Select(
            //         $@"Server={textBox1.Text};Database={textBox2.Text};Uid={textBox3.Text};Pwd={textBox4.Text};",
            //         dataGridView1);
            // }
            // catch (Exception exception)
            // {
            //     MessageBox.Show(exception.Message);
            //     throw;
            // }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conStr = (@"Server=" + textBox1.Text + ";Database=" + textBox2.Text + ";Uid=" +
                             textBox3.Text + ";Pwd=" + textBox4.Text);
            saveFileDialog1.Filter = @"BIN (*.bin)|";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = File.CreateText($"{saveFileDialog1.FileName}.bin"))
                {
                    writer.Write(conStr);
                }
            }
        }

        private void BTTest_Connection_Click(object sender, EventArgs e)
        {
            if (_dbManager == null)
            {
                MessageBox.Show("Failed connection");
                return;
            }

            if (_dbManager.CheckConnection()) MessageBox.Show("Test successfully");
            else MessageBox.Show("Failed connection");
        }

        private void BTCreate_Click(object sender, EventArgs e)
        {
            _dbManager.CreateTable("Prikhodchenko", 
                "Code int not null primary key, " +
                "AnalysisName varchar(100) not null," +
                "Price double not null check(Price > 0.01)");
            BTCreate.Enabled=false;
            CBSwitcher.Enabled = true;
            BTDrop_Table.Enabled = true;
            button2.Enabled = true;
        }

        private void BTDrop_Table_Click(object sender, EventArgs e)
        {
            _dbManager.DropTable("Prikhodchenko");
            BTCreate.Enabled = true;
            BTDrop_Table.Enabled = false;
            CBSwitcher.Checked= false;
            CBSwitcher.Enabled=false;
            button2.Enabled = false;
        }

        private void CBFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (CBFilter.Checked)
                _dbManager.Select(@"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", dataGridView1);
            else
                _dbManager.SelectAll("INFORMATION_SCHEMA.TABLES", dataGridView1);
        }

        private void CBSwitcher_CheckedChanged(object sender, EventArgs e)
        {
            if (CBSwitcher.Checked)
            {
                _dbManager.Select(@"SELECT * FROM Prikhodchenko", dataGridView1);
                CBFilter.Enabled = false;
            }
            else
            {
                _dbManager.Select(@"SELECT * FROM INFORMATION_SCHEMA.TABLES", dataGridView1);
                CBFilter.Enabled=true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.ExecuteSql($"insert into Prikhodchenko(code, analysisname, price) values ( {Convert.ToInt32(textBox5.Text)}," +
                                  $"'{textBox6.Text}', {Convert.ToDouble(textBox7.Text)})");
            _dbManager.Select(@"SELECT * FROM Prikhodchenko", dataGridView1);
        }
    }
}