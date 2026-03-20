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



        

        protected MSSqlDb MSAccountDb => new MSSqlDb(Properties.Settings.Default.CONN_STRING_MS_ACCOUNT);
        //protected MSSqlDb MSAccountDb => new MSSqlDb(Properties.Settings.Default.CONN_STRING_MS_ACCOUNT);




    }
}
