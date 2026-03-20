using System;
using System.Collections.Generic;
using System.Text;
using HAHATalk.Models;
using System.Threading.Tasks;

namespace HAHATalk.Repositories
{
    public interface IChatRepository
    {
        // 안 읽은 메세지 총합 가져오기 (비동기 방식) 
        Task<int> MSSQL_GetTotalUnrealCountAsync(string email);

        // 채팅방 목록 가져오기 (비동기) 
        Task<List<ChatList>> MSSQL_GetChatListAsync(string email);
    }
}
