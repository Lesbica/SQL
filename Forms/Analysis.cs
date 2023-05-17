using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Analysis : Form
    {
        private DbManager _dbManager;
        
        public Analysis()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Analysis_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("analysis", dataGridView1);
        }
    }
}