using CommonLib.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAHATalk.Repositories
{
    // 추상클래스
    public abstract class RepositoryBase
    {
        protected MySqlDb AccountDb => new MySqlDb(Properties.Settings.Default.CONN_STRING_ACCOUNT);



        string MSSQL_connectionString = string.Format("Server = 127.0.0.1, 9008; Uid = root; Pwd = 1357; database = hahatalk; TrustServerCertificate=True;Encrypt=False;");

        protected MSSqlDb MSAccountDb => new MSSqlDb(MSSQL_connectionString);
        //protected MSSqlDb MSAccountDb => new MSSqlDb(Properties.Settings.Default.CONN_STRING_MS_ACCOUNT);




    }
}
