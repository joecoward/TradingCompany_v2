
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.Entity
{
    public class RoleEntity
    {
        [Key]
        public int RoleId { get; set; }
        [Column("role_name")]
        public string? RoleName { get; set; }
        [InverseProperty("Role")]
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    }
}
