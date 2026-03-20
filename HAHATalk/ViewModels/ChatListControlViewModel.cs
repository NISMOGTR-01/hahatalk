using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Models;
using HAHATalk.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class ChatListControlViewModel
    {
        // 트리거가 감시할 이름표 
        public string ControlName => "ChatList";

        private readonly INavigationService _navigationService;

        // 채팅방 목록을 담는 콜렉션 
        [ObservableProperty]
        private ObservableCollection<ChatList> _chatList = new();

        public ChatListControlViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            // 우선 테스트용 더미 데이터를 제작 
            //LoadDummyData();
        }

      
        // 채팅방 클릭할 경우 해당 방으로 이동하는 RelayCommand 
        [RelayCommand]
        public void OpenChatRoom(ChatList selectedList)
        {
            if (selectedList == null)
                return;

            // chatRoom으로 이동하는 
            _navigationService.Navigate(NaviType.ChatRoom);
        }
    }
}
