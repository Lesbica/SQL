using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Lab_assistant : Form
    {
        private DbManager _dbManager;
        
        public Lab_assistant()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Lab_assistant_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("labassistant", dataGridView1);
        }
    }
}