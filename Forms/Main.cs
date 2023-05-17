using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Visitor visitor = new Visitor();
            visitor.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Result result = new Result();
            result.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Reagents_in_analysis reagentsInAnalysis = new Reagents_in_analysis();
            reagentsInAnalysis.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Reagent reagent = new Reagent();
            reagent.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Laborant_analysis laborantAnalysis = new Laborant_analysis();
            laborantAnalysis.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lab_assistant labAssistant = new Lab_assistant();
            labAssistant.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Analysis_Type analysisType = new Analysis_Type();
            analysisType.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Analysis_Orders analysisOrders = new Analysis_Orders();
            analysisOrders.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Analysis analysis = new Analysis();
            analysis.ShowDialog();
        }
    }
}