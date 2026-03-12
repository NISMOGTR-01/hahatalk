using CommonLib.DataBase;
using HAHATalk.Models;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAHATalk.Repositories
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        // 회원가입 계정정보를 저장하는 함수 (MYSQL Server) 
        public long Save(Account account)
        {
            string query = "" +
                "INSERT account SET\n" +
                "  id = @id\n" +
                ", pwd = @pwd\n" +
                ", email = @email\n" +
                ", nickname = @nickname\n" +
                ", cell_phone = @cell_phone";

            using (MySqlDb db = AccountDb)
            {
                return db.Execute(query, new SqlParameter[]
                 {
                    new SqlParameter("@id", account.Id),
                    new SqlParameter("@pwd", account.Pwd),
                    new SqlParameter("@email", account.Email),
                    new SqlParameter("@nickname", account.Nickname),
                    new SqlParameter("@cell_phone", account.CellPhone),
                 });
            }
        }

        /*
        // 회원가입 계정정보를 저장하는 함수 (MSSQL Server) 
        public long MSSQL_Save(Account account)
        {
            string query = "" +
                "INSERT INTO account (\n" +
                 "  id = @id\n" +
                ", pwd = @pwd\n" +
                ", email = @email\n" +
                ", nickname = @nickname\n" +
                ", cell_phone = @cell_phone";

            using (MSSqlDb db = MSAccountDb)
            {
                return db.Execute(query, new SqlParameter[]
                {
                    new SqlParameter("@id", account.Id),
                    new SqlParameter("@pwd", account.Pwd),
                    new SqlParameter("@email", account.Email),
                    new SqlParameter("@nickname", account.Nickname),
                    new SqlParameter("@cell_phone", account.CellPhone),
                });
            }
        }
        */

        // 저장하는 이메일이 현재 존재하는지 확인하는 함수 (MYSQL Server) 
        public bool ExistEmail(string email)
        {
            string query =
              "SELECT 1 FROM account\n" +
              "WHERE email = @email";
            using MySqlDb db = AccountDb;
            using System.Data.IDataReader? dr = db.GetReader(query, new SqlParameter[] { new SqlParameter("@email", email) });
            return dr.Read();
        }

        public bool MSSQL_ExistEmail(string email)
        {
            string query =
                   "SELECT 1 FROM account\n" +
                   "WHERE email = @email";
            using MSSqlDb db = MSAccountDb;
            using System.Data.IDataReader? dr = db.GetReader(query, new SqlParameter[] { new SqlParameter("@email", email) });


            return dr.Read();
        }

        public long MSSQL_Save(Account account)
        {
            string query = @"
                INSERT INTO account (
                    pwd, email, nickname, cell_phone
                ) VALUES (
                    @pwd, @email, @nickname, @cell_phone
                );";

            using (MSSqlDb db = MSAccountDb)
            {
                return db.Execute(query, new SqlParameter[]
                {
                    //new SqlParameter("@id", account.Id),
                    new SqlParameter("@pwd", account.Pwd),
                    new SqlParameter("@email", account.Email),
                    new SqlParameter("@nickname", account.Nickname),
                    new SqlParameter("@cell_phone", account.CellPhone),
                });
            }
        }

            /*
            // 저장하는 이메일이 현재 존재하는지 확인하는 함수 (MSSQL Server)
            public bool MSSQL_ExistEmail(string email)
            {
                string query =
                    "SELECT 1 FROM account\n" +
                    "WHERE email = @email";
                using MSSqlDb db = MSAccountDb;
                using System.Data.IDataReader? dr = db.GetReader(query, new SqlParameter[] { new SqlParameter("@email", email) });


                return dr.Read();
            }
            */
        }
}
