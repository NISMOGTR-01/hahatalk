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
               
                ", pwd = @pwd\n" +
                ", email = @email\n" +
                ", nickname = @nickname\n" +
                ", cell_phone = @cell_phone";

            using (MySqlDb db = AccountDb)
            {
                return db.Execute(query, new SqlParameter[]
                 {
                    
                    new SqlParameter("@pwd", account.Pwd),
                    new SqlParameter("@email", account.Email),
                    new SqlParameter("@nickname", account.Nickname),
                    new SqlParameter("@cell_phone", account.CellPhone),
                 });
            }
        }

        

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

        // 변수가 필요함 
        public long MSSQL_Pass_Update(Account account, string changePwd)
        {
            string query = "UPDATE account SET pwd = @pwd WHERE email = @email;";

            using (MSSqlDb db = MSAccountDb)
            {
                return db.Execute(query, new SqlParameter[]
                {
                    //new SqlParameter("@id", account.Id),
                    new SqlParameter("@pwd", changePwd),
                    new SqlParameter("@email", account.Email),
                    
                });
            }
        }

        public string? MSSQL_Find_Account(Account account)
        {
            string query = "SELECT email FROM account WHERE cell_phone = @cell_phone";

            using (MSSqlDb db = MSAccountDb)
            {
                // db.ExecuteScalar를 사용하여 단일 값을 가져오는 방식이 세진님이 쓰시는 db 객체에 있다면 가장 깔끔합니다.
                // 만약 ExecuteScalar가 없다면 아래처럼 GetReader를 사용하세요.
                using (var dr = db.GetReader(query, new SqlParameter[]
                {
                    new SqlParameter("@cell_phone", account.CellPhone.Replace("-", "")),
                }))
                {
                    if (dr.Read())
                    {
                        return dr["email"].ToString();
                    }
                }
            }

            // 계정을 찾지 못하면 0 반환 
            return "0";
        }

        // 로그인 체크 함수 (2026.03.16) 
        public bool MSSQL_Login_Check(string email, string pwd)
        {
            // 패스워드까지 한번에 조회해서 계정이 존재하는지 확인 
            string query = "SELECT 1 FROM account WHERE email = @email AND PWD = @pwd";

            using (MSSqlDb db = MSAccountDb)
            {
                using (var dr = db.GetReader(query, new SqlParameter[] {
                        new SqlParameter("@email", email), 
                        new SqlParameter("@pwd", pwd)

                    }))
                {
                    return dr.Read();   // dr에 레코드가 존재하면 true, 없으면 false return 
                }
            }
        }

        // 
        public Account? MSSQL_GetAccountByEmail(string email)
        {
            string query = "SELECT email, nickname FROM account WHERE email = @email";

            using (MSSqlDb Db = MSAccountDb)
            {
                using (var dr = Db.GetReader(query, new SqlParameter[] { new SqlParameter("@email", email)}))
                {
                    if(dr.Read())
                    {
                        return new Account
                        {
                          
                            Email = dr["email"].ToString()!,
                            Nickname = dr["nickname"].ToString()!
                        };
                    }
                }
            }

            return null;
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
    }
}
