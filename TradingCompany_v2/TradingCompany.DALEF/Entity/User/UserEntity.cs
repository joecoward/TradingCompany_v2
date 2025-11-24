using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.Entity.User
{
    [Index(nameof(Login), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        [Column("login")]
        public string? Login { get; set; }
        [Required]
        [StringLength(100)]
        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public byte[] Password { get; set; }
        [Column("salt")]
        public byte[] Salt { get; set; }

        [NotMapped]
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public DateTime UpdatedAt { get; set; }

        [InverseProperty("User")]
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    }
}
