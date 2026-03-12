using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CommonLib.DataBase
{
    public class MySqlDb : IDisposable
    {
        private MySqlConnection? _conn;
        private readonly string _connectionString;

        private void Connection()
        {
            _conn = new MySqlConnection(_connectionString);
            try
            {
                _conn.Open();
            }
            catch(Exception ex)
            {
                // LOG 
                Console.WriteLine(ex.ToString());
            }
        }

        private void AddParameters(MySqlCommand cmd, SqlParameter[]? parameters)
        {
            if(parameters != null)
            {
                foreach(SqlParameter param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                }
            }
        }

        public MySqlDb(string connectionString)
        {
            _connectionString = connectionString;
            Connection();
        }

        public IDataReader GetReader(string query)
        {
            return GetReader(query, null);
        }

        public IDataReader GetReader(string query, SqlParameter[]? parameters)
        {
            using MySqlCommand cmd = new MySqlCommand(query, _conn);
            AddParameters(cmd, parameters);
            return cmd.ExecuteReader();
        }

        // dataTable 
        public DataTable GetDataTable(string query)
        {
            return GetDataTable(query, null);
        }

        public DataTable GetDataTable(string query, SqlParameter[]? parameters)
        {
            using MySqlCommand cmd = new MySqlCommand(query, _conn);
            AddParameters(cmd, parameters);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }



        public long Execute(string query)
        {
            return Execute(query, null);
        }

        public long Execute(string query, SqlParameter[]? parameters)
        {
            using MySqlCommand cmd = new MySqlCommand(query, _conn);
            AddParameters(cmd, parameters);
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }


        public void Dispose()
        {
            _conn?.Close(); // connection 해제 
            _conn?.Dispose(); // connection 메모리 해제 
            _conn = null;      // _conn : null
            GC.SuppressFinalize(this);  // 가비지 콜렉터에서 수집 제외 
        }
    }
}
