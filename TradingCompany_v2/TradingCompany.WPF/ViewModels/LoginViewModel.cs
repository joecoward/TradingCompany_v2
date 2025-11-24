using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradingCompany.WPF.Core;
namespace TradingCompany.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _login;
        private readonly Services.Interfases.IAuthentication _authenticationService;
        public event Action LoginSuccess;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public RelayCommand LoginCommand { get; }
        public LoginViewModel(Services.Interfases.IAuthentication authenticationService)
        {
            _authenticationService = authenticationService;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }
        private void ExecuteLogin(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;
            if( _authenticationService.ValidateUser(Login, password))
            {
                LoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль!");
            }
        }
    }
}
