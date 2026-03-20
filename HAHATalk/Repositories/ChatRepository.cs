using CommonLib.DataBase;
using HAHATalk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HAHATalk.Repositories
{
    public class ChatRepository : RepositoryBase, IChatRepository
    {
        // 채팅목록 가져오기 (비동기)        
        public async Task<List<ChatList>> MSSQL_GetChatListAsync(string email)
        {
            string query = "SELECT * FROM ChatList WHERE OwnerId = @email ORDER BY LastTime DESC ";

            return await Task.Run(() =>
            {
                List<ChatList> list = new List<ChatList>();
                using (MSSqlDb db = MSAccountDb)
                {
                    using (IDataReader dr = db.GetReader(query, new SqlParameter[] { new SqlParameter("email", email) }))
                    {
                        if(dr.Read())
                        {
                            list.Add(new ChatList
                            {
                                TargetName = dr["TargetName"].ToString()!, 
                                LastMessage = dr["LastMessage"].ToString()!, 
                                LastTime = Convert.ToDateTime(dr["LastTime"]), 
                                UnreadCount = Convert.ToInt32(dr["UnreadCount"]), 
                                ProfileImg = dr["ProfileImg"]?.ToString()!
                            });
                        }   
                    }

                }

                return list;
            });
        
        }
        

        public async Task<int> MSSQL_GetTotalUnrealCountAsync(string email)
        {
            // 모든 채팅방의 안 읽은 메세지 총합(비동기) 
            string query = "SELECT ISNULL(SUM(UnreadCount), 0) FROM ChatList WHERE OwnerId = @email";

            // 비동기로 실행하기 위해 Task.Run을 활용하거나, 
            // 만약 db.GetReaderAsync가 없다면 아래와 같이 구성합니다.
            return await Task.Run(() =>
            {
                using(MSSqlDb db = MSAccountDb)
                {
                    using(IDataReader dr = db.GetReader(query, new SqlParameter[] {new SqlParameter ("@email", email) }))
                    {
                        if(dr.Read())
                        {
                            return Convert.ToInt32(dr[0]);
                        }
                    }
                }

                return 0;
            });
        }
    }
}
