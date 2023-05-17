using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SQL
{
    public class DbManager
    {
        private const string ConStr = @"Server=localhost;Database=laboratoria;Uid=root;Pwd=11111;";
        private MySqlConnection _mySqlConnection = new MySqlConnection();
        private MySqlCommand _mySqlCommand = new MySqlCommand();
        public DbManager()
        {
            _mySqlCommand.Connection = _mySqlConnection;
        }

        private void FillGrid(DataGridView dataGridView, MySqlDataReader myR)
        {
            dataGridView.Columns.Clear();
            for (int i = 0; i < myR.FieldCount; i++)
            {
                dataGridView.Columns.Add("Col" + i, myR.GetName(i));
            }

            while (myR.Read())
            {
                object[] s = new object[myR.FieldCount];
                for (int i = 0; i < myR.FieldCount; i++)
                {
                    s[i] = myR[i].ToString();
                }

                dataGridView.Rows.Add(s);
            }
        }

        
        public void SelectAll(string tablename, DataGridView dataGridView)
        {
            try
            {
                _mySqlConnection.Open();
                _mySqlCommand.CommandText = "select * from " + tablename;
                MySqlDataReader myR = _mySqlCommand.ExecuteReader();
                FillGrid(dataGridView, myR);
                _mySqlConnection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public void ConnectTo(string pconStr)
        {
            _mySqlConnection.ConnectionString = pconStr;
        }        
        
        public void ConnectTo()
        {
            _mySqlConnection.ConnectionString = ConStr;
        }

        public List<List<Object>> SelectAll(string tableName)
        {
            try
            {
                var res = new List<List<Object>>();
                _mySqlCommand.CommandText = "select * from " + tableName;
                _mySqlConnection.Open();
                MySqlDataReader reader = _mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    List<Object> row = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i]);
                    }
                    res.Add(row);
                }
                _mySqlConnection.Close();
                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}