using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class FindAccountControlViewModel
    {
        public FindAccountControlViewModel()
        {
            
        }
        /*
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
        public void NavigateLogin()
        {
            _navigationService.Navigate(NaviType.Login);
        }

        public FindAccountControlViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        */
    }
}
