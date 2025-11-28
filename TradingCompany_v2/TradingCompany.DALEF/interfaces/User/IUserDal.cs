using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.interfaces.User
{
    public interface IUserDal
    {
        public UserDTO Create(UserDTO user, string password);
        public UserDTO GetById(int id);
        public List<UserDTO> GetAll();
        public UserDTO Update(UserDTO user);
        public void Delete(int id);
        public UserDTO GetUserByLogin(string login);

        public UserDTO GetUserByEmail(string email);
        public bool ValidateUser(string login, string password);
    }
}
