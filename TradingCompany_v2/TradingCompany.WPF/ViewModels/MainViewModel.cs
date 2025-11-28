using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.WPF.Core;
using TradingCompany.WPF.Services.Concrete;

namespace TradingCompany.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        private readonly IServiceProvider _serviceProvider;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ShowLogin();
        }

        private void ShowLogin()
        {
            var loginVm = _serviceProvider.GetRequiredService<LoginViewModel>();

            loginVm.LoginSuccess += () => ShowUserHome();

            loginVm.Registration += () => ShowRegistration();

            CurrentView = loginVm;
        }

        private void ShowRegistration()
        {
            var registrationVm = _serviceProvider.GetRequiredService<RegistrationViewModel>();
            registrationVm.Cancel += () => ShowLogin();
            registrationVm.RegisterSuccess += () => ShowUserHome();
            CurrentView = registrationVm;
        }

        private void ShowUserHome()
        {
            var homeVm = _serviceProvider.GetRequiredService<UserHomeViewModel>();
            homeVm.LogoutRequested += () => ShowLogin();
            CurrentView = homeVm;
        }
    }
}
