using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Windows;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.Concrete.User;
using TradingCompany.DALEF.interfaces.User;
using TradingCompany.DALEF.MapperProfiles;
using TradingCompany.WPF.Services.Concrete;
using TradingCompany.WPF.Services.Interfases;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF
{
    public partial class App : Application
    {
        static IMapper _mapper;

        private ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TradingCompanyContext>(opt =>
                opt.UseSqlServer(connectionString)
                   .EnableSensitiveDataLogging());

            services.AddLogging(builder => 
            {
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            services.AddScoped<IUserDal>(provider => new UserDal(connectionString, provider.GetRequiredService<IMapper>()));

            services.AddSingleton(provider =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                }, loggerFactory);

                return config.CreateMapper(provider.GetService);
            });



            services.AddSingleton<IAuthentication, Authentication>();


            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<HomeViewModel>();


            services.AddSingleton<MainWindow>();
        }

        // ОСЬ ЦЕЙ МЕТОД, ПРО ЯКИЙ ВИ ПИТАЛИ
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ми просимо DI дати нам готове вікно з усім необхідним
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}