using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Result : Form
    {
        private DbManager _dbManager;
        
        public Result()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Result_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("result", dataGridView1);
        }
    }
}