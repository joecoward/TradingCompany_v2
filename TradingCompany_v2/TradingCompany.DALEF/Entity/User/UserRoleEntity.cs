using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DALEF.Entity.User;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.Entity
{
    public class UserRoleEntity
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public UserEntity? User { get; set; } 
        public RoleEntity? Role { get; set; }
    }
}
