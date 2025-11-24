using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingCompany.WPF.Core;

namespace TradingCompany.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public event Action LogoutRequested;

        public string WelcomeMessage { get; set; } = "Вітаємо в системі!";

        public ICommand LogoutCommand { get; }

        public HomeViewModel()
        {
            LogoutCommand = new RelayCommand(ExecuteLogout);
        }

        private void ExecuteLogout(object obj)
        {
            // Смикаємо за "мотузочку", повідомляючи MainViewModel
            LogoutRequested?.Invoke();
        }
    }
}
