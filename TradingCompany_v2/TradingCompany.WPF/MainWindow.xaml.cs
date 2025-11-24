using System.Windows;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF
{
    public partial class MainWindow : Window
    {
        // Змінюємо конструктор: приймаємо MainViewModel як параметр
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();

            // Прив'язуємо отриману ViewModel до контексту даних вікна
            DataContext = mainViewModel;
        }
    }
}