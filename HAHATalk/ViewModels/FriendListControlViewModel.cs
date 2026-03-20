using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Controls;
using HAHATalk.Models;
using HAHATalk.Repositories;
using HAHATalk.Services;
using HAHATalk.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class FriendListControlViewModel
    {
        // 트리거가 감시할 이름표 
        public string ControlName => "FriendList";

        // 서비스 주입을 위한 필드 
        private readonly INavigationService _navigationService;
        private readonly IFriendRepository _friendRepository;   // 2026.03.19 FriendRepository 추가 

        // 1.XAML의 ItemsControl ItemsSource={Binding Friends}
        [ObservableProperty]
        private ObservableCollection<Friend> _friends;

        // 2.XAML의 TextBlock Text={Binding FriendsCountText}
        [ObservableProperty]
        private string _friendsCountText = "";

        // 3.채팅 사이드바 배지에 표시할 미확인 메시지 총합 
        [ObservableProperty]
        private UserStore? _userStore;       // 사용자 정보 

        // 2026.03.19 추가 (팝업 가시성 및 입력데이터) -> 친구 등록 
        [ObservableProperty]
        private bool _isAddFriendVisible;   // 팝업 /열기 닫기 (BoolToVis 컨버터 연결) 
        [ObservableProperty]
        private string _newFriendName = "";
        [ObservableProperty]
        private string _newFriendEmail = "";
        [ObservableProperty]
        private string _newFriendPhone = "";



        public FriendListControlViewModel(INavigationService navigationService, UserStore userStore, IFriendRepository friendRepository)
        {
            this._navigationService = navigationService;
            this._userStore = userStore;
            this._friendRepository = friendRepository;

            // 초기 데이터 세팅
            Friends = new ObservableCollection<Friend>();
            LoadFriends(); // 친구 목록을 불러오는 별도 메서드 호출

        }

        // 친구 목록 불러오는 로직 (나중에 Repository를 주입받으면 DB 연동으로 바꿀 부분)
        private void LoadFriends()
        {
            // UserStore에서 로그인 시 저장해둔 ID를 가져온다. 
            string myId = _userStore.CurrentUserId;
            
            if (string.IsNullOrEmpty(myId))
            {
                FriendsCountText = "로그인 정보가 없습니다.";
                return;
            }
            
            // DB에서 친구 목록 가져오기 
            var dbFriends = _friendRepository.MSSQL_GetFriends(myId);

            // UI 스레드에서 컬렉션 업데이트 
            Friends.Clear();
            if (dbFriends != null)
            {
                foreach (var friend in dbFriends)
                {
                    Friends.Add(friend);
                }
            }

            // 상단 카운트 텍스트 업데이트 
            FriendsCountText = $"친구 {Friends.Count}명";
        }

        // 2026.03.20 추가 
        [RelayCommand]
        //private void AddFriend() => IsAddFriendVisible = true; // 팝업 띄우기 
        private void AddFriend()
        {
            try
            {
                // 1. 창 인스턴스 생성
                var win = new AddFriendControl();

                // 2. 데이터 컨텍스트 연결
                win.DataContext = this;

                // 3. 부모 창 설정 (이게 간혹 문제를 일으키면 주석 처리하고 테스트해 보세요)
                // win.Owner = System.Windows.Application.Current.MainWindow;

                // 4. 창 띄우기
                win.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"에러 발생: {ex.Message}");
            }
        }

        [RelayCommand]
        private void CloseAddFriend()
        {
            ClearAddFriendFields();
            IsAddFriendVisible = false; // 팝업 닫기 
        }

        [RelayCommand]
        private async Task ConfirmAddFriend()
        {
            if (string.IsNullOrWhiteSpace(NewFriendName) || string.IsNullOrWhiteSpace(NewFriendEmail))
                return;

            // [Step 1] 중복 체크 먼저 수행!
            bool isAlreadyCheck = await _friendRepository.IsFriendAlreadyExistsAsync(_userStore.CurrentUserId, NewFriendEmail);

            if (isAlreadyCheck == true)
            {
                MessageBox.Show("이미 등록된 친구입니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // 중복이면 여기서 중단!
            }

            // 1. DB 저장 (Repository 활용)
            var result = await _friendRepository.AddFriendAsync(_userStore.CurrentUserId, NewFriendEmail, NewFriendName,
                "");

            if(result == false)
            {
                MessageBox.Show("친구등록에 실패했습니다.");
                return;

            }

            // 2. UI 즉시 반영 (임시 객체 생성) 
            var newFriend = new Friend
            {
                MyEmail = NewFriendEmail,   // 등록하려는 친구 이메일
                TargetEmail = _userStore.CurrentUserId, // 로그인 계정 정보 (나중에 목록을 가져와야) 
                FriendName = NewFriendName, // 닉네임 
                StatusMsg = ""

            };

            Friends.Add(newFriend);
            FriendsCountText = $"친구 {Friends.Count}명";

            // 3. 필드 초기화 및 닫기 
            CloseAddFriend();
        }

        private void ClearAddFriendFields()
        {
            NewFriendName = "";
            NewFriendEmail = "";
            NewFriendPhone = "";
            
        }

        [RelayCommand]
        private void NavigateToChatList()
        {
            // _navigationService를 통해 채팅 목록 화면으로 이동 
            _navigationService.Navigate(NaviType.ChatList);
        }

        [RelayCommand]
        private void NavigateToSettings()
        {
            // 나중에 설정 화면이 생기면 이동하는 로직
            // _navigationService.Navigate(NaviType.Settings);
        }

        // 친구 갱신 
        [RelayCommand]
        private void RefreshFriends()
        {
            LoadFriends();
        }

        // 2026.03.19 친구 검색, 친구 등록 추가 
        [RelayCommand]
        private void Search()
        {

        }
    }
}
