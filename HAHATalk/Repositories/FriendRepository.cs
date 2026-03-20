using CommonLib.DataBase;
using HAHATalk.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAHATalk.Repositories
{
    public class FriendRepository : RepositoryBase, IFriendRepository
    {
        public async Task<bool> AddFriendAsync(string myId, string friendEmail, string friendName, string statusMsg)
        {
            string query = @"
                INSERT INTO Friends (my_email, target_email, friend_name, status_msg)
                VALUES (@my_email, @target_email, @friend_name, @status_msg)";

            try
            {
                using (MSSqlDb db = MSAccountDb)
                {
                    // 
                    long rowsAffected = await Task.Run(() => db.Execute(query, new SqlParameter[]
                    {
                        new SqlParameter("@my_email", myId),
                        new SqlParameter("@target_email", friendEmail),
                        new SqlParameter("@friend_name", friendName),
                        new SqlParameter("@status_msg", statusMsg),
                    }));

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddFriendAsync Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsFriendAlreadyExistsAsync(string myId, string friendEmail)
        {
            string query = @"
                SELECT COUNT(*)
                FROM Friends 
                WHERE my_email = @my_email AND target_email = @target_email";

            try
            {
                using (MSSqlDb db = MSAccountDb)
                {
                    object result = await Task.Run(() => db.GetReader(query, new SqlParameter[]
                    {
                    new SqlParameter("@my_email", myId),
                    new SqlParameter("@target_email", friendEmail),

                    }));

                    return Convert.ToInt32(result) > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                return false;
            }

        }

        // MSSQL 서버에서 친구를 가져오는 메소드 
        public List<Friend> MSSQL_GetFriends(string myEmail)
        {
            List<Friend> list = new List<Friend>();

            // friend Table 쿼리 조회 
            string query = @"
                SELECT my_email, target_email, friend_name, status_msg
                FROM Friends
                WHERE my_email = @my_email";

            // RepositoryBase에서 상속받는 MSAccountDb 사용 
            using (MSSqlDb db = MSAccountDb)
            {
                using (var dr = db.GetReader(query, new SqlParameter[]
                {
                    new SqlParameter("@my_email", myEmail)
                }))
                {
                    while (dr.Read())
                    {
                        list.Add(new Friend
                        {
                            MyEmail = dr["my_email"].ToString()!,
                            TargetEmail = dr["target_email"].ToString()!,
                            FriendName = dr["friend_name"].ToString()!,
                            StatusMsg = dr["status_msg"].ToString()!,
                        });
                    }
                }
            }

            return list;
        }



    }   

}