using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingCompany.DALEF.Entity
{
    [Table("Categories")] 
    public class CategoryEntity
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("type")] 
        public string? Name { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}