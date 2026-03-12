using HAHATalk.Models;

namespace HAHATalk.Repositories
{
    public interface IAccountRepository
    {
        //bool ExistEmail(string email);
        bool ExistEmail(string email);
        bool MSSQL_ExistEmail(string email);

        long Save(Account account);



        long MSSQL_Save(Account account);


    }
}