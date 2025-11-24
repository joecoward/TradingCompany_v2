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
        private readonly Services.Interfases.IAuthentication _authService;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel(Services.Interfases.IAuthentication authentication)
        {
            _authService = authentication;
            ShowLogin();
        }

        private void ShowLogin()
        {
            var loginVm = new LoginViewModel(_authService);
            loginVm.LoginSuccess += () => CurrentView = new HomeViewModel(); // Перехід на Home при успіху
            CurrentView = loginVm;
        }
    }
}
