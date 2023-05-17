using System;
using System.Windows.Forms;

namespace SQL
{
    public partial class Laborant_analysis : Form
    {
        private DbManager _dbManager;
        
        public Laborant_analysis()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Laborant_analysis_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("laborantanalysis", dataGridView1);
        }
    }
}