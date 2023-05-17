using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Analysis_Orders : Form
    {
        private DbManager _dbManager;
        
        public Analysis_Orders()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Analysis_Orders_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("analysisorders", dataGridView1);
        }
    }
}