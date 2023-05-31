using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Laborant_analysis : Form
    {
        private DbManager _dbManager;
        
        public Laborant_analysis()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Laborant_analysis_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("laborantanalysis", dataGridView1);
            _dbManager.FillComboBoxWithData("analysis", "AnalCode", "Price", comboBox1);
            _dbManager.FillComboBoxWithData("labassistant", "ID", "FullName", comboBox2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            var selectedItem2 = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;

            _dbManager.ExecuteSql($"insert into laborantanalysis(analcode, labassistant) values ('{selectedItem}', " +
                                  $"'{selectedItem2}')");
            _dbManager.SelectAll("laborantanalysis", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            _dbManager.Delete("laborantanalysis", "analcode="+selectedItem);
            comboBox2.Text = "";
            comboBox1.Text = "";
            _dbManager.SelectAll("laborantanalysis", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("laborantanalysis", "");
            _dbManager.SelectAll("laborantanalysis", dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            
            var AnalCode = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            var LabAssistant = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;

            _dbManager.ExecuteSql($"UPDATE laborantanalysis SET AnalCode = {AnalCode}, LabAssistant = {LabAssistant} " +
                                  $"WHERE AnalCode = {dataGridView1.Rows[rowIndex].Cells[0].Value}");
            
            _dbManager.SelectAll("laborantanalysis", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            KeyValuePair<int, string> desiredItem = null;
            foreach (KeyValuePair<int, string> item in comboBox1.Items)
            {
                if (item.Key == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value))
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
                if (item.Key == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
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
    }
}