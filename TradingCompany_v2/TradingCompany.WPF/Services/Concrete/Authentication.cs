
using TradingCompany.WPF.Services.Interfases;
using TradingCompany.DALEF.interfaces.User;
using TradingCompany.DTO.User;
using System.Threading.Tasks;

namespace TradingCompany.WPF.Services.Concrete
{
    public class Authentication : IAuthentication
    {
        private readonly IUserDal _userDal;
        private readonly IUserRoleDal _userRoleDal;
        public Authentication(IUserDal userDal,IUserRoleDal userRoleDal)
        {
            _userDal = userDal;
            _userRoleDal = userRoleDal;
        }

        public bool RegisterUser(string login, string email, string password)
        {
            var existingUser = _userDal.GetUserByLogin(login);

            if (existingUser != null)
            {
                return false;
            }

            var userByEmail = _userDal.GetUserByEmail(email);
            if (userByEmail != null)
            {
                return false;
            }

            var entity = new UserDTO
            {
                Login = login,
                Email = email
            };

            var newUser = _userDal.Create(entity, password);
            if (newUser != null)
            {
                _userRoleDal.Create(newUser);
                return true;
            }
            return false;
        }

        public bool ValidateUser(string login, string password)
        {
           return _userDal.ValidateUser(login, password);
        }
    }
}
