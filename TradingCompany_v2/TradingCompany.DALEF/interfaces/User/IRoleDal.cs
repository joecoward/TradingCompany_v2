using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.interfaces.User
{
    public interface IRoleDal
    {
        public RoleDTO Create(RoleDTO role);
        public RoleDTO GetById(int id);
        public List<RoleDTO> GetAll();
        public RoleDTO Update(RoleDTO role);
        public void Delete(int id);
    }
}
