using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.WPF.Core;

namespace TradingCompany.WPF.Models
{
    public class LoginModel : ViewModelBase , IDataErrorInfo
    {
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Login))
                {
                    if (string.IsNullOrWhiteSpace(Login))
                    {
                        return "Login cannot be empty.";
                    }
                }
                return null;
            }
        }
        public bool IsValid => string.IsNullOrWhiteSpace(this[nameof(Login)]);

    }
}
