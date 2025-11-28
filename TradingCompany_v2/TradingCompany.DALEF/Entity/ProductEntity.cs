using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingCompany.DALEF.Entity
{
    [Table("Products")]
    public class ProductEntity
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryEntity? Category { get; set; }

        public ICollection<ActionProductEntity> ActionProducts { get; set; } = new List<ActionProductEntity>();
    }
}