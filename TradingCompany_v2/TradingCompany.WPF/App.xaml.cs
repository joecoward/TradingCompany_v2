using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Windows;
using TradingCompany.DALEF.Concrete;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.Concrete.User;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DALEF.interfaces.User;
using TradingCompany.DALEF.MapperProfiles;
using TradingCompany.WPF.Services.Concrete;
using TradingCompany.WPF.Services.Interfases;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF
{
    public partial class App : Application
    {

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

            services.AddSingleton<IUserDal>(provider => new UserDal(connectionString, provider.GetRequiredService<IMapper>()));
            services.AddSingleton<IUserRoleDal>(provider => new UserRoleDal(connectionString, provider.GetRequiredService<IMapper>()));
            services.AddSingleton<IProductDal>(provider => new ProductDal(connectionString, provider.GetRequiredService<IMapper>()));
            services.AddSingleton<IActionDal>(provider => new ActionDal(connectionString, provider.GetRequiredService<IMapper>()));
            services.AddSingleton<ICategoryDal>(provider => new CategoryDal(connectionString, provider.GetRequiredService<IMapper>()));
            services.AddSingleton<IStatusDal>(provider => new StatusDal(connectionString, provider.GetRequiredService<IMapper>()));

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
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<UserHomeViewModel>();


            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            this.MainWindow = mainWindow;
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}