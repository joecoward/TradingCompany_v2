
using TradingCompany.WPF.Services.Interfases;
using TradingCompany.DALEF.interfaces.User;

namespace TradingCompany.WPF.Services.Concrete
{
    public class Authentication : IAuthentication
    {
        private readonly IUserDal _userDal;
        public Authentication(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public bool ValidateUser(string login, string password)
        {
           return _userDal.ValidateUser(login, password);
        }
    }
}
