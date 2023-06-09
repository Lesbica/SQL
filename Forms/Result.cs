﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    public partial class Result : Form
    {
        private DbManager _dbManager;
        
        public Result()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void Result_Load(object sender, EventArgs e)
        {
            _dbManager.SelectAll("result", dataGridView1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _dbManager.ExecuteSql($"insert into result(coderesult, shortinfo, longinfo) values ({Convert.ToInt32(textBox1.Text)}, '{textBox2.Text}', '{textBox3.Text}')");
            _dbManager.SelectAll("result", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("result", "coderesult="+textBox1.Text);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }
            }
            _dbManager.SelectAll("result", dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _dbManager.Delete("result", "");
            _dbManager.SelectAll("result", dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            int CodeResult = Convert.ToInt32(textBox1.Text);
            string ShortInfo = textBox2.Text;
            string LongInfo = textBox3.Text;
            

            _dbManager.ExecuteSql($"UPDATE result SET CodeResult = {CodeResult}, ShortInfo = {ShortInfo}, LongInfo = {LongInfo} " +
                                  $"WHERE CodeResult = {dataGridView1.Rows[rowIndex].Cells[0].Value}");
            
            _dbManager.SelectAll("result", dataGridView1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Введіть дані");
                    checkBox1.Checked = false;
                    return;
                }
                
                Dictionary<string, object> searchParameters = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    searchParameters.Add("CodeResult", Convert.ToInt32(textBox1.Text));
                }

                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    searchParameters.Add("ShortInfo", textBox2.Text);
                }

                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    searchParameters.Add("LongInfo", textBox3.Text);
                }

                DataTable resultTable = _dbManager.SearchData(searchParameters, "result");

                dataGridView1.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = resultTable;

                dataGridView1.DataSource = bindingSource;
            }
            else
            {
                dataGridView1.DataSource = null;
                _dbManager.SelectAll("result", dataGridView1);
            }
        }
        
    }
}