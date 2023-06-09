﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    public partial class Analysis : Form
    {
        private DbManager _dbManager;
        
        public Analysis()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Analysis_Load(object sender, EventArgs e)
        {
           _dbManager.SelectAll("analysis", dataGridView1);

            _dbManager.FillComboBoxWithData("analysistype", "CodeAnalType", "NameAnalType", comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            _dbManager.ExecuteSql($"insert into analysis(AnalCode, AnalysisType, Price) values ({Convert.ToInt32(textBox1.Text)}, {selectedItem}, {textBox3.Text})");
            _dbManager.SelectAll("analysis", dataGridView1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("analysis", "AnalCode="+textBox1.Text);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                    comboBox1.Text = "";
                }
            }
            _dbManager.SelectAll("analysis", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("analysis", "");
            _dbManager.SelectAll("analysis", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                
                KeyValuePair<int, string> desiredItem = null;
                foreach (KeyValuePair<int, string> item in comboBox1.Items)
                {
                    if (item.Key == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                    {
                        desiredItem = item;
                        break;
                    }
                }
                
                if (desiredItem != null)
                {
                    comboBox1.SelectedItem = desiredItem;
                }
                
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            int analcode = Convert.ToInt32(textBox1.Text);
            double price = Convert.ToDouble(textBox3.Text);
            var analysistype = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;

            _dbManager.ExecuteSql($"UPDATE analysis SET AnalCode = {analcode}, AnalysisType = {analysistype}, Price = {price} " +
                                  $"WHERE AnalCode = {dataGridView1.Rows[rowIndex].Cells[0].Value}");
            
            _dbManager.SelectAll("analysis", dataGridView1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Dictionary<string, object> searchParameters = new Dictionary<string, object>();
                if (textBox1.Text.Length != 0)
                {
                    searchParameters.Add("AnalCode", Convert.ToInt32(textBox1.Text));
                }

                if (comboBox1.SelectedItem != null)
                {
                    searchParameters.Add("AnalysisType", ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key);
                }

                if (textBox3.Text.Length != 0)
                {
                    searchParameters.Add("Price", Convert.ToDouble(textBox3.Text));
                }

                if (textBox1.Text.Length == 0 && comboBox1.SelectedItem == null && textBox3.Text.Length == 0)
                {
                    MessageBox.Show("Введіть дані");
                    checkBox1.Checked = false;
                    return;
                }

                DataTable resultTable = _dbManager.SearchData(searchParameters, "analysis");

                dataGridView1.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = resultTable;

                dataGridView1.DataSource = bindingSource;
            }
            else
            {
                dataGridView1.DataSource = null;
                _dbManager.SelectAll("analysis", dataGridView1);
            }
        }

    }
}