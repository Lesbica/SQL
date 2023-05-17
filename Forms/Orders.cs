using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Orders : Form
    {
        private DbManager _dbManager;
        
        public Orders()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("orders", dataGridView1);
        }
    }
}