using HAHATalk.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAHATalk.Repositories
{
    public interface IFriendRepository
    {
        // 로그인 한 계정의 ID를 매개변수로 받아 친구목록(LIST)를 반환
        List<Friend> MSSQL_GetFriends(string myId);

        // 친구 추가 등록
        Task<bool> AddFriendAsync(string myId, string friendEmail, string friendName, string statusMsg); 

        // 친구 중복 확인 
        Task<bool> IsFriendAlreadyExistsAsync(string myId, string friendEmail);


    }
}
