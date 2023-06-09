using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Dapper;
using MySql.Data.MySqlClient;

namespace SQL
{
    public class DbManager
    {
        private string ConStr = @"Server=localhost;Database=laboratoria;Uid=root;Pwd=11111;Charset=utf8mb4;";
        private static MySqlConnection _mySqlConnection = new MySqlConnection();
        private static MySqlCommand _mySqlCommand = new MySqlCommand();
        private static DbManager instance;
        public DbManager()
        {
            _mySqlCommand.Connection = _mySqlConnection;
        }       
        
        public DbManager(string conString)
        {
            _mySqlConnection = new MySqlConnection(conString);
            _mySqlCommand = new MySqlCommand();
            _mySqlCommand.Connection = _mySqlConnection;
            ConStr=conString;
        }
        
        public bool CheckConnection()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConStr))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DbManager getInstance()
        {
            if (instance == null)
                instance = new DbManager();
            return instance;
        }
        public static DbManager getInstance(string conStr)
        {
            if (instance == null)
                instance = new DbManager(conStr);
            return instance;
        }

        public static void deleteInstance()
        {
            instance=null;
        }
        
        public void Select(string query,DataGridView dataGridView)
        {
            DataTable dt = new DataTable();
            _mySqlCommand.CommandText= query;
            _mySqlConnection.Open();
            MySqlDataReader myR = _mySqlCommand.ExecuteReader();
            FillGrid(dataGridView, myR);
            _mySqlConnection.Close();
        }
        
        public void ExecuteTransaction(uint count, int reagcode, char oper)
        {
            _mySqlConnection.Open();
            MySqlTransaction transaction;
            
            transaction = _mySqlConnection.BeginTransaction();
            _mySqlCommand.Transaction = transaction;

            try
            {
                _mySqlCommand.Parameters.Clear();
                if (oper == '-')
                {
                    _mySqlCommand.CommandText = "UPDATE Reagent SET Available = Available - @count WHERE ReagCode = @reagcode";
                    _mySqlCommand.Parameters.AddWithValue("@count", count);
                    _mySqlCommand.Parameters.AddWithValue("@reagcode", reagcode);
                    _mySqlCommand.ExecuteNonQuery();
                
                    transaction.Commit(); 
                    MessageBox.Show("Транзакция выполнена успешно.");
                    _mySqlConnection.Close();
                }
                else if (oper == '+')
                {
                    _mySqlCommand.CommandText = "UPDATE Reagent SET Available = Available + @count WHERE ReagCode = @reagcode";
                    _mySqlCommand.Parameters.AddWithValue("@count", count);
                    _mySqlCommand.Parameters.AddWithValue("@reagcode", reagcode);
                    _mySqlCommand.ExecuteNonQuery();
                
                    transaction.Commit(); 
                    MessageBox.Show("Транзакция выполнена успешно.");
                    _mySqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _mySqlConnection.Close();
                MessageBox.Show("Ошибка выполнения транзакции: " + ex.Message);
            }
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
        
        public static string ReadConnectionStringFromFile(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            string connectionString = System.Text.Encoding.UTF8.GetString(bytes);
            return connectionString;
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

        public void ExecuteSql(string query)
        {
            try
            {
                _mySqlCommand.CommandText = query;
                _mySqlConnection.Open();
                _mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }
        
        public void Insert(string tableName, String[] fields, String[] values)
        {
            try
            {
                _mySqlCommand.CommandText = "insert into "+tableName+"("+String.Join(", ", fields)+") values("+String.Join(", ",values)+")";
                _mySqlConnection.Open();
                _mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }
        
        public void Delete(string tableName, String condition)
        {
            try
            {
                if (condition!="")
                {
                    _mySqlCommand.CommandText = "delete from "+tableName+" where "+condition;
                }
                else
                {
                    _mySqlCommand.CommandText = "delete from " + tableName;
                }
                _mySqlConnection.Open();
                _mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }
        
        public void CreateTables(List<string> tableQueries)
        {
            try
            {
                foreach (string query in tableQueries)
                {
                    ExecuteSql(query);
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        
        public void CreateTable(string tableName, string columns)
        {
            try
            {

                _mySqlConnection.Open();

                string createTableQuery = $"CREATE TABLE {tableName} ({columns})";
                

                _mySqlCommand.CommandText = createTableQuery;
                _mySqlCommand.ExecuteNonQuery();

                MessageBox.Show($"Таблица {tableName} успешно создана.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }
     
        public void DropTables()
        {
            try
            {
                _mySqlConnection.Open();
                
                var schema = _mySqlConnection.GetSchema("Tables");
                var tableNames = new List<string>();

                foreach (DataRow row in schema.Rows)
                {
                    var tableName = row["TABLE_NAME"].ToString();
                    tableNames.Add(tableName);
                }
                _mySqlConnection.Close();
                foreach (var tableName in tableNames)
                {
                    DropAllForeignKeys(tableName);
                    DropTable(tableName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public void DropTable(string tableName)
        {
            DropAllForeignKeys(tableName);
            ExecuteSql("DROP TABLE " + tableName);
        }

        public bool TableExists(string tableName)
        {
            try
            {
                _mySqlConnection.Open();

                string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";

                _mySqlCommand.CommandText = query;
                int count = Convert.ToInt32(_mySqlCommand.ExecuteScalar());

                return count > 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }

        
        public void CreateForeignKey(string tableName, string columnName, string referencedTableName, string referencedColumnName)
        {
            try
            {
                _mySqlConnection.Open();

                string query = $"ALTER TABLE @tableName ADD CONSTRAINT FK_{tableName}_{columnName} FOREIGN KEY (@columnName) REFERENCES @referencedTableName(@referencedColumnName)";

                _mySqlCommand.CommandText = query;
                _mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }

        private void DropForeignKey(string tableName, string foreignKeyName)
        {
            try
            {
                _mySqlConnection.Open();

                string query = $"ALTER TABLE {tableName} DROP FOREIGN KEY {foreignKeyName}";

                ExecuteSql(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public void DropAllForeignKeys(string tableName)
        {
            try
            {
                _mySqlConnection.Open();

                string query1 = $"SELECT CONSTRAINT_NAME as ConstraintName, TABLE_NAME, REFERENCED_TABLE_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = @tableName AND REFERENCED_TABLE_NAME IS NOT NULL";
                
                _mySqlCommand.CommandText = query1;
                var result1 = _mySqlConnection.Query<KeyColumnUsage>(query1, new { tableName });

                string query2 = $"SELECT DISTINCT TABLE_NAME as TABLE_NAME, CONSTRAINT_NAME as ConstraintName FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE REFERENCED_TABLE_NAME = @tableName";

                _mySqlCommand.CommandText = query2;
                var result2 = _mySqlConnection.Query<KeyColumnUsage>(query2, new { tableName });
                
                foreach (var keyColumnUsage in result2)
                {
                    string foreignKeyName =keyColumnUsage.ConstraintName;
                    string referencedTable = keyColumnUsage.TABLE_NAME;

                    string dropForeignKeyQuery = $"ALTER TABLE {referencedTable} DROP FOREIGN KEY {foreignKeyName}";

                    _mySqlConnection.Execute(dropForeignKeyQuery);
                }
                
                foreach (var keyColumnUsage in result1)
                {
                    string foreignKeyName =keyColumnUsage.ConstraintName;
                    string referencedTable = keyColumnUsage.REFERENCED_TABLE_NAME;
                 
                    string dropForeignKeyQuery = $"ALTER TABLE {tableName} DROP FOREIGN KEY {foreignKeyName}";
                 
                    _mySqlConnection.Execute(dropForeignKeyQuery);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }

        public bool HasForeignKeys(string tableName)
        {
            try
            {
                _mySqlConnection.Open();

                string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE TABLE_NAME = '{tableName}'";

                _mySqlCommand.CommandText = query;
                int count = Convert.ToInt32(_mySqlCommand.ExecuteScalar());
        
                return count > 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }
        
        public void FillComboBoxWithData(string tableName, string codeColumnName, string nameColumnName, ComboBox comboBox)
        {
            try
            {
                comboBox.Items.Clear();
        
                string query = $"SELECT {codeColumnName}, {nameColumnName} FROM {tableName}";

                _mySqlCommand.CommandText = query;
                _mySqlConnection.Open();
                MySqlDataReader reader = _mySqlCommand.ExecuteReader();
                
                while (reader.Read())
                {
                    int code = Convert.ToInt32(reader.GetString(codeColumnName));
                    string name = reader.GetString(nameColumnName);
                    comboBox.Items.Add(new KeyValuePair<int, string>(code, name));
                }

                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }

        public DataTable Select(String query)
        {
            try
            {
                _mySqlConnection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, _mySqlConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }    
        
        public DataTable SearchData(Dictionary<string, object> parameters, string tableName)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (MySqlConnection connection = new MySqlConnection(ConStr))
                {
                    connection.Open();

                    string query = $"SELECT * FROM {tableName} WHERE ";
                    List<string> conditions = new List<string>();

                    foreach (var parameter in parameters)
                    {
                        string condition = $"{parameter.Key} = @{parameter.Key}";
                        conditions.Add(condition);
                    }

                    query += string.Join(" AND ", conditions);

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue($"@{parameter.Key}", parameter.Value);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                return dataTable;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            
        }
        
        public DataTable ExecuteQuery(string query, string value)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConStr))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        char[] delimiters = { '@' };

                        string[] words = query.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        if (words.Length > 1)
                        {
                            string result = words[1];
                            command.Parameters.AddWithValue("@"+result, value);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }

            return dataTable;
        }
        
        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(ConStr))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                connection.Close();
            }

            return dataTable;
        }
        
        
    }
    

    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Key}, {Value}]";
        }
    }


    public class KeyColumnUsage
    {
        public string ConstraintName { get; set; }
        public string TABLE_NAME { get; set; }
        public string REFERENCED_TABLE_NAME { get; set; }
    }
}