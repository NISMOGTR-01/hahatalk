using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Repositories;
using HAHATalk.Services;
using HAHATalk.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class LoginControlViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;
        private readonly IChatRepository _chatRepository;
        private readonly UserStore _userStore;

        [ObservableProperty]
        private ObservableCollection<string>? _emails;

        [ObservableProperty]
        private string _selectedEmail = ""; // 화면에서 선택하거나 입력한 이메일 

        [ObservableProperty]
        private string _password = "";  // 입력받을 패스워드

        [ObservableProperty]
        private string _validationText = "";    // 에러 메세지 표시용 

        // 2026.03.17 프로퍼티 추가 
        [ObservableProperty]
        private bool _isLoggingIn;

        // 생성자에서 필요한 서비스를 주입받는다. (의존성 주입) 
        public LoginControlViewModel(INavigationService navigationService, IAccountRepository accountRepository, IChatRepository chatRepository, 
            UserStore userStore)
        {
            this._navigationService = navigationService;
            this._accountRepository = accountRepository;
            this._chatRepository = chatRepository;
            this._userStore = userStore;

            Emails = new ObservableCollection<string>()
            {
                "test1@test.com",
                "test2@test.com",
                "test3@test.com",
            };

            SelectedEmail = Emails.FirstOrDefault()!;
            _chatRepository = chatRepository;
        }

        [RelayCommand]
        //private void Login(object obj)
        private async Task Login(object obj)        // 2026.03.17 버튼 애니메이션 효과를 위해 비동기 형식으로 변경 
        {
            // 넘어온 프로젝트가 PasswordBoxControl인지 확인하고 패스워드 추출 
            if(obj is WPFLib.Controls.PasswordBoxControl pwControl)
            {
                this.Password = pwControl.Password;
            }

            // 1. 입력 유효성 검사
            if (string.IsNullOrEmpty(SelectedEmail) || string.IsNullOrEmpty(Password))
            {
                ValidationText = "이메일과 비밀번호를 모두 입력해주세요.";
                return;
            }

            // 2. DB 로그인 체크
            IsLoggingIn = true;
            ValidationText = "로그인 중...";
            
            bool isSuccess = await Task.Run(() => _accountRepository.MSSQL_Login_Check(SelectedEmail, Password));

            if(isSuccess)
            {
                // 로그인 성공시 안 읽은 메세지 합산 가져오기 
                ValidationText = "사용자 데이터를 불러오는 중...";

                // 
                // 2026.03.19
                var account = await Task.Run(() => _accountRepository.MSSQL_GetAccountByEmail(SelectedEmail));
                // 계정이 null 이 아니면 계정정보를 가져옴 
                if(account != null)
                {
                    
                    _userStore.CurrentUserEmail = account.Email;    // 현재 이메일 
                    _userStore.CurrentUserId = account.Email;       // 현재 이메일 
                    _userStore.CurrentUserNickname = account.Nickname;

                }

                // 3. 비동기로 UnreadCount 합산
                int totalUnread = await _chatRepository.MSSQL_GetTotalUnrealCountAsync(SelectedEmail);
            
                await Task.Delay(1000);                
                
                _navigationService.Navigate(NaviType.FriendList);
            }
            else
            {
                ValidationText = "이메일 또는 비밀번호가 올바르지 않습니다.";
                IsLoggingIn = false;
            }
        }
       
    }
}
