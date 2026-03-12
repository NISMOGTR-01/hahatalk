using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class ChangePwdControlViewModel
    {
        public ChangePwdControlViewModel()
        {
            
        }
        /*
        private readonly INavigationService _navigationService;

        [RelayCommand]
        public void NavigateFindAccount()
        {
            _navigationService.Navigate(NaviType.FindAccount);
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


        public ChangePwdControlViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        */
    }
}
