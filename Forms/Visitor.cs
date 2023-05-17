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

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Visitor_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("visitor", dataGridView1);
        }
    }
}