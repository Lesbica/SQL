using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Visitor : Form
    {
        private dbManager _dbManager;
        public Visitor()
        {
            InitializeComponent();
            _dbManager = new dbManager();
            _dbManager.ConnectTo();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _dbManager.SelectAll("visitor", dataGridView1);
        }
    }
}