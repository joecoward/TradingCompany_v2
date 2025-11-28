using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TradingCompany.WPF.Core;
using TradingCompany.WPF.Models;
namespace TradingCompany.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly Services.Interfases.IAuthentication _authenticationService;
        private string _errorMessage;

        public event Action LoginSuccess;
        public event Action Registration;


        public LoginModel Login { get; set; } = new LoginModel();
        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }


        public LoginViewModel(Services.Interfases.IAuthentication authenticationService)
        {
            _authenticationService = authenticationService;
            LoginCommand = new RelayCommand(ExecuteLogin);
            RegistrationCommand = new RelayCommand(ExecuteRegistration);
        }
        private void ExecuteLogin(object parameter)
        {
            ErrorMessage = string.Empty;

            if (!Login.IsValid)
            {
                ErrorMessage = "Please correct the errors before proceeding.";
                return;
            }


            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Password cannot be empty.";
                return;
            }

            if ( _authenticationService.ValidateUser(Login.Login, password))
            {
                LoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show("Incorrect login or password!");
            }
        }
        private void ExecuteRegistration(object obj)
        {
            Registration?.Invoke();
        }
    }
}
