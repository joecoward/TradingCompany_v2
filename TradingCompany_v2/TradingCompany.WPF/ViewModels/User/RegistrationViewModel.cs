using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TradingCompany.WPF.Core;
using TradingCompany.WPF.Models;

namespace TradingCompany.WPF.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _errorMessage;

        private readonly Services.Interfases.IAuthentication _authenticationService;
        
        public RegistrationModel Registration { get; set; } = new RegistrationModel();

        public event Action Cancel;
        public event Action RegisterSuccess;

        public ICommand CancelCommand { get; }
        public ICommand RegisterCommand { get; }


        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public RegistrationViewModel(Services.Interfases.IAuthentication authenticationService)
        {
            _authenticationService = authenticationService;
            CancelCommand = new RelayCommand(ExecuteCancle);
            RegisterCommand = new RelayCommand(ExecuteRegister);
        }

        private void ExecuteCancle(object obj)
        {
            Cancel?.Invoke();
        }
        private void ExecuteRegister(object parameter)
        {
            ErrorMessage = "";
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;
            if (string.IsNullOrWhiteSpace(Registration.Login)||
                string.IsNullOrWhiteSpace(Registration.Email)||
                string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "All fields must be filled in!";
                return;
            }

            if (!Registration.IsValid)
            {
                ErrorMessage = "Incorrect format Email!";
                return;
            }

            if (password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters long!";
                return;
            }

            try
            {
                bool success = _authenticationService.RegisterUser(Registration.Login, Registration.Email, password);

                if (success)
                {
                    RegisterSuccess?.Invoke();
                }
                else
                {
                    ErrorMessage = "A user with this login already exists!";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Server error. Please try again later.";
            }

        }
    }
}
