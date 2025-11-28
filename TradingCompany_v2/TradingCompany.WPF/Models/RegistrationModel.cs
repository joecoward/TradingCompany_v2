using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.WPF.Core;

namespace TradingCompany.WPF.Models
{
    public class RegistrationModel : ViewModelBase , IDataErrorInfo
    {
        private string _login;
        private string _email;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
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
                else if (columnName == nameof(Email))
                {
                    if (string.IsNullOrWhiteSpace(Email))
                    {
                        return "Email cannot be empty.";
                    }
                    else if (!Email.Contains("@gmail") || !Email.Contains("."))
                    {
                        return "Email format is invalid.";
                    }
                }
                return null;
            }
        }
        public bool IsValid=> string.IsNullOrWhiteSpace(this[nameof(Login)]) && string.IsNullOrWhiteSpace(this[nameof(Email)]);

    }
}
