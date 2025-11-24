using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.interfaces.User
{
    public interface IUserRoleDal
    {
        public UserRoleDTO Create(UserRoleDTO userRole);
        public UserRoleDTO GetById(int id);
        public List<UserRoleDTO> GetAll();
        public UserRoleDTO Update(UserRoleDTO userRole);
        public void Delete(int id);
    }
}
