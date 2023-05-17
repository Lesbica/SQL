using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Reagent : Form
    {
        private DbManager _dbManager;
        
        public Reagent()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Reagent_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("reagent", dataGridView1);
        }
    }
}