using HAHATalk.Repositories;
using HAHATalk.Services;
using HAHATalk.Stores;
using HAHATalk.ViewModels;
using HAHATalk.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HAHATalk
{

    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            Startup += App_Startup;          
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // 생성자 주입 
            var mainView = App.Current.Services.GetService<MainView>()!;
            mainView.Show();
        }

        public new static App Current => (App)Application.Current;

    
        public IServiceProvider Services { get; }

      
        // 서비스 등록 및 설정 
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //stores
            services.AddSingleton<MainNavigationStore>();
            services.AddSingleton<UserStore>();         // 2026.03.17 Add UserStore

            // services
            services.AddSingleton<INavigationService, NavigationService>();

            // Repositories
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();   //2026.03.17 ChatRespository 추가 
            services.AddTransient<IFriendRepository, FriendRepository>();


            //services.AddSingleton<ITestService, TestService>();

            // ViewModels 
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginControlViewModel>();
            services.AddTransient<SignupControlViewModel>();
            services.AddTransient<ChangePwdControlViewModel>();
            services.AddTransient<FindAccountControlViewModel>();
            services.AddTransient<MainNaviControlViewModel>();

            // 2026.03.17 FriendListControlViewModel 추가 
            services.AddTransient<FriendListControlViewModel>();
            // 2026.03.17 ChatListControlViewModel 추가 
            services.AddTransient<ChatListControlViewModel>();

            // Views 
            services.AddSingleton(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });
            //services.AddSingleton<MainView>();


            return services.BuildServiceProvider();
        }
    }

}
