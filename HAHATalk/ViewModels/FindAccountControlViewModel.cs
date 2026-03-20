using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HAHATalk.Models;
using HAHATalk.Repositories;
using HAHATalk.Services;
using Org.BouncyCastle.Pqc.Crypto.Falcon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HAHATalk.ViewModels
{
    [ObservableObject]
    public partial class FindAccountControlViewModel
    {
        // 2026.03.16 추가 
        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;

        // Properties 
        [ObservableProperty]
        private string _cellPhone = default!;   // 전화번호 

        [ObservableProperty]
        private string _validationText = default!;   // 전화번호 유효성 체크 결과 표시 텍스트 (전화번호로 계정이 있는지 없는지) 


        [RelayCommand]
        private void FindAccount()
        {
            if (CellPhone == "" || CellPhone == null)
            {
                ValidationText = "전화번호를 입력하세요.";
                return;
            }

            // 전화번호로 가입한 계정이 있는지 확인 
            Find_Account();
            
        }

      

       

        private void ClearValidating()
        {
            ValidationText = "";
        }

        public FindAccountControlViewModel(INavigationService navigationService, IAccountRepository accountRepository)
        {
            this._navigationService = navigationService;
            this._accountRepository = accountRepository;
        }


        // 실제 계정을 찾는 메소드 
        private void Find_Account()
        {


            Account account = GetAccount();
            
            // MSSQL
            // 인터페이스 수정 
            string strAccount = _accountRepository.MSSQL_Find_Account(account);

            if(strAccount == "0")
            {
                this.ValidationText = "가입되지 않은 전화번호입니다.";
            }
            else
            {
                this.ValidationText = $"계정은 {strAccount} 입니다.";
            }
        }

        private Account GetAccount() => new() { CellPhone = CellPhone};

      
    }
}
