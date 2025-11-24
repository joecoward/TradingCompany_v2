using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DTO.User
{
    public class UserRoleDTO
    {
        public int UserRoleId { get; set; }
        public List<UserDTO> User { get; set; } = new List<UserDTO>();
        public List<RoleDTO> Role { get; set; } = new List<RoleDTO>();
    }
}
