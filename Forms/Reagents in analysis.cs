using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Reagents_in_analysis : Form
    {
        private DbManager _dbManager;
        
        public Reagents_in_analysis()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Reagents_in_analysis_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("reagentsinanalysis", dataGridView1);
        }
    }
}