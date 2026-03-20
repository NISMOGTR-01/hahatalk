using HAHATalk.Models;

namespace HAHATalk.Repositories
{
    public interface IAccountRepository
    {
        //bool ExistEmail(string email);
        bool ExistEmail(string email);
        bool MSSQL_ExistEmail(string email);

        long Save(Account account);

        long MSSQL_Save(Account account);       // 2026.03.12

        long MSSQL_Pass_Update(Account account, string newPwd);     // 2026.03.11 업데이트 쿼리 추가 

        string? MSSQL_Find_Account(Account account);    // 2026.03.16 계정 찾기 쿼리 추가 

        bool MSSQL_Login_Check(string email, string pwd);

        Account? MSSQL_GetAccountByEmail(string email); // 2026.03.19 입력한 아이디로 사용자 정보 계정 가져오기 
    }
}