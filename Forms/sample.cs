using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SQL
{
    public partial class sample : Form
    {
        
        private DbManager _dbManager;
        public sample()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            string query = "SELECT AnalysisType.NameAnalType, Analysis.Price " +
                           "FROM Visitor " +
                           "JOIN Orders ON Visitor.ID = Orders.Visitors " +
                           "JOIN AnalysisOrders ON Orders.OrderNum = AnalysisOrders.OrderNum " +
                           "JOIN Analysis ON AnalysisOrders.AnalCode = Analysis.AnalCode " +
                           "JOIN AnalysisType ON Analysis.AnalysisType = AnalysisType.CodeAnalType " +
                           "WHERE Visitor.FullName = @fullName";
            DataTable resultTable = _dbManager.ExecuteQuery(query, textBox1.Text);
            dataGridView1.DataSource = resultTable;
            decimal summ = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                summ += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
            }

            label3.Text = summ.ToString(CultureInfo.InvariantCulture);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            string query = "SELECT Reagent.NameReagent AS Name_Reagent, " +
                           "Reagent.Available AS Available, " +
                           "AnalysisType.NameAnalType AS Name_Analysis, " +
                           "ReagentsInAnalysis.UsedCount, (Reagent.Available - ReagentsInAnalysis.UsedCount) AS Remaining " +
                           "FROM Reagent " +
                           "INNER JOIN ReagentsInAnalysis ON Reagent.ReagCode = ReagentsInAnalysis.ReagCode " +
                           "INNER JOIN Analysis ON ReagentsInAnalysis.AnalCode = Analysis.AnalCode " +
                           "INNER JOIN AnalysisType ON Analysis.AnalysisType = AnalysisType.CodeAnalType";
            DataTable resultTable = _dbManager.ExecuteQuery(query);
            dataGridView1.DataSource = resultTable;
        }
    }
}