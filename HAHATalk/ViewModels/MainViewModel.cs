using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Stores;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using WPFLib.Controls;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class MainViewModel
    {
        [ObservableProperty]
        public INotifyPropertyChanged _currentViewModel = default!;

        [ObservableProperty]
        private SlideType _slideType = default!;

        private void CurrentViewModelChanged(INotifyPropertyChanged viewModel)
        {
            CurrentViewModel = viewModel;
        }

        public MainViewModel(MainNavigationStore mainNavigationStore)
        {
            mainNavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;
            mainNavigationStore.SlideTypeChanged += SlideTypeChanged;
            // 처음에 시작하는 메인 화면은 Login화면으로 할당 
            mainNavigationStore.CurrentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(LoginControlViewModel))!;
            // 생성자를 로그인 
            //_currentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(LoginControlViewModel))!;
        }

        private void SlideTypeChanged(SlideType slideType)
        {
            SlideType = slideType;
        }

        /*
        [RelayCommand]
        public void ToLogin()
        {
            CurrentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(LoginControlViewModel))!;
        }

        [RelayCommand]
        public void ToChangePwd()
        {
            CurrentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(ChangePwdControlViewModel))!;
        }

        [RelayCommand]
        public void ToSignup()
        {
            CurrentViewModel = (INotifyPropertyChanged)App.Current.Services.GetService(typeof(SignupControlViewModel))!;
        }
        */



    }
}
