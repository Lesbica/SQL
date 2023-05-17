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
    }
}