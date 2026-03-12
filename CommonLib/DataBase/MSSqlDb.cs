using Microsoft.Data.SqlClient; // MySql 대신 사용
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;




namespace CommonLib.DataBase
{
    public class MSSqlDb : IDisposable
    {
        private SqlConnection? _conn;
        private readonly string _connectionString;

        private void Connection()
        {
            _conn = new SqlConnection(_connectionString);

            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // MSQL 전용 SqlCommand와 SqlParameter를 사용하도록 수정 
        private void AddParameters(SqlCommand cmd, SqlParameter[]? parameters)
        {
            foreach(SqlParameter param in parameters)
            {
                // 이름이 @로 시작하는지 체크해서, 안 붙어 있으면 붙여준 뒤 추가 
                string name = param.ParameterName.StartsWith("@")
                    ? param.ParameterName
                    : "@" + param.ParameterName;

                // MSSQL에서는 이미 존재하는 객체의 이름은 바꿀수 없으므로, 
                // 새로운 이름과 기존 값을 사용하여 커맨드에 추가 
                cmd.Parameters.AddWithValue(name, param.Value);
            }
        }

        public MSSqlDb(string connectionString)
        {
            _connectionString = connectionString;
            Connection();
        }

        // Overload 1: 매개 변수 없는 GetReader
        public IDataReader GetReader(string query)
        {
            return GetReader(query, null);
        }

        // 매개 변수 있는 GetReader(IDataReader 변환) 
        public IDataReader GetReader(string query, SqlParameter[]? parameters)
        {
            SqlCommand cmd = new SqlCommand(query, _conn);
            AddParameters(cmd, parameters);
            return cmd.ExecuteReader();
        }

        // overload1 매개변수 없는 GetDataTable 
        public DataTable GetDataTable(string query)
        {
            return GetDataTable(query, null);
        }

        // Overload 2: 매개 변수 있는 GetDataTable (DataTable 변환) 
        public DataTable GetDataTable(string query, SqlParameter[]? parameters)
        {
            using SqlCommand cmd = new SqlCommand(query, _conn);
            AddParameters(cmd, parameters);
            
            using SqlDataAdapter da = new SqlDataAdapter(cmd);
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
            using SqlCommand cmd = new SqlCommand(query, _conn);
            AddParameters(cmd, parameters);

            return (long)cmd.ExecuteNonQuery();
        }

        public void Dispose() 
        {
            if (_conn != null && _conn.State != ConnectionState.Closed)
            {
                _conn.Close();
            }
            _conn?.Dispose();
            _conn = null;
            GC.SuppressFinalize(this);

        }
    }
}
