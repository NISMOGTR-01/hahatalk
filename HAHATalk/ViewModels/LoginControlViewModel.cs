using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class LoginControlViewModel
    {
        [ObservableProperty]
        private ObservableCollection<string>? _emails;

        public LoginControlViewModel()
        {
            Emails = new ObservableCollection<string>()
            {
                "test1@test.com",
                "test2@test.com",
                "test3@test.com",
            };
            
        }
        /*
        [ObservableProperty]
        private ObservableCollection<string>? _emails;
        private readonly INavigationService _navigationService;

        [RelayCommand]
        public void NavigateChangePassword()
        {
            _navigationService.Navigate(NaviType.ChangePwd);
        }

        [RelayCommand]
        public void NavigateSignup()
        {
            _navigationService.Navigate(NaviType.Signup);
        }

        [RelayCommand]
        public void NavigateFindAccount()
        {
            _navigationService.Navigate(NaviType.FindAccount);
        }

        public LoginControlViewModel(INavigationService navigationService)
        {
            Emails = new ObservableCollection<string>
            {
                "test1@test.com",
                "test2@test.com",
                "test3@test.com",
            };

            this._navigationService = navigationService;
        }
        */
    }
}
