using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TradingCompany.DALEF.MapperProfiles;
using TradingCompany.DTO.User;
using TradingCompany.DALEF.interfaces.User;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.Concrete.User;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DALEF.Concrete;

namespace TradingCompany_v2
{
    public class Program
    {
        static string _connectionString;
        static IMapper _mapper;
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            var services = new ServiceCollection();

            services.AddLogging(b =>
            {
                b.ClearProviders();
                b.AddConsole();
                b.SetMinimumLevel(LogLevel.Information);
            });

            services.AddDbContext<TradingCompanyContext>(opt =>
                opt.UseSqlServer(_connectionString)
                   .EnableSensitiveDataLogging());

            services.AddScoped<IUserDal>(provider=> new UserDal(_connectionString, provider.GetRequiredService<IMapper>()));
            services.AddScoped<IActionDal>(provider => new ActionDal(_connectionString, provider.GetRequiredService<IMapper>()));

            services.AddSingleton(provider =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                }, loggerFactory);

                return config.CreateMapper(provider.GetService);
            });

            var provider = services.BuildServiceProvider();


            Console.WriteLine("Welcome to TradingCompany!");
            while (true)
            {
                Console.WriteLine("\nSelect tables:");
                Console.WriteLine("1 - Prodoucts table");
                Console.WriteLine("2 - Actions table");
                Console.WriteLine("3 - ActionProduct table");
                Console.WriteLine("4 - Category table");
                Console.WriteLine("5 - Status table");
                Console.WriteLine("6 _ User table");
                Console.WriteLine("q - Quit");
                Console.Write("Your choice: ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var c = char.ToLowerInvariant(line[0]);
                switch (c)
                {
                    case '1':

                        break;
                    case '2':
                        ActionMenu(provider);
                        break;
                    case '3':

                        break;
                    case '4':

                        break;
                    case '5':

                        break;
                    case '6':
                        UserMenu(provider);
                        break;
                    case 'q':
                        Console.WriteLine("Bye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }




            }
        }

        private static void ActionMenu(ServiceProvider provider)
        {
            while (true)
            {
                Console.WriteLine("\nType:");
                Console.WriteLine("1 - Get all Action");
                Console.WriteLine("2 - Add a Action");
                Console.WriteLine("3-  Update Action");
                Console.WriteLine("4 - Delete Action");
                Console.WriteLine("q - Quit");
                Console.Write("Your choice: ");

                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var c = char.ToLowerInvariant(line[0]);


                switch (c)
                {
                    case '1':
                        using (var scope = provider.CreateScope())
                        {
                            var actionDal = scope.ServiceProvider.GetRequiredService<IActionDal>();
                            var actions = actionDal.GetAll();
                            foreach (var action in actions)
                            {
                                Console.WriteLine("--------------------------------------------------");
                                Console.WriteLine($" ID: {action.ActionId},\n Name: {action.Name},\n Description: {action.Description},\n StartDate: {action.StartDate},\n EndDate: {action.EndDate},\n StatusID: {action.Status.StatusId}");
                            }
                        }
                        break;

                    case '2':
                        
                        break;

                    case '3':

                        break;

                    case '4':

                        break;

                    case 'q':
                        Console.WriteLine("Bye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private static void UserMenu(ServiceProvider provider)
        {
            while (true)
            {
                Console.WriteLine("\nType:");
                Console.WriteLine("1 - Get all User");
                Console.WriteLine("2 - Create a User");
                Console.WriteLine("3-  Update User");
                Console.WriteLine("4 - Delete User");
                Console.WriteLine("5 - login in");
                Console.WriteLine("q - Quit");
                Console.Write("Your choice: ");

                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var c = char.ToLowerInvariant(line[0]);


                switch (c)
                {
                    case '1':

                        break;

                    case '2':
                        using (var scope = provider.CreateScope())
                        {
                            var userDal = scope.ServiceProvider.GetRequiredService<IUserDal>();

                            Console.Write("Enter Login: ");
                            var login = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            var email = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            var password = Console.ReadLine();
                            var userDto = new UserDTO
                            {
                                Login = login,
                                Email = email
                            };
                            var createdUser = userDal.Create(userDto, password);
                            Console.WriteLine($"User created with ID: {createdUser.UserId}");
                        }
                        break;

                    case '3':

                        break;

                    case '4':

                        break;
                    case '5':
                        using (var scope = provider.CreateScope())
                        {
                            var userDal = scope.ServiceProvider.GetRequiredService<IUserDal>();

                            Console.Write("Enter Login: ");
                            var login = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            var password = Console.ReadLine();
                            var login_in = userDal.ValidateUser(login, password);
                            if (login_in != null)
                            {
                                Console.WriteLine("Login successful! Welcome");
                            }
                            else
                            {
                                Console.WriteLine("Invalid login or password.");
                            }

                        }
                        break;
                    case 'q':
                        Console.WriteLine("Bye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
