using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DALEF.Entity.User;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.Entity
{
    [Table("User_Roles")]
    public class UserRoleEntity
    {
        [Key]
        [Column("user_roles_id")]
        public int UserRoleId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }

        public UserEntity? User { get; set; }

        public RoleEntity? Role { get; set; }
    }
}
