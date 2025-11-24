using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingCompany.DALEF.Entity
{
    [Table("ActionProducts")] // Перевірте, чи таблиця називається ActionProducts або action_products
    public class ActionProductEntity
    {
        [Key]
        [Column("actionproduct_id")]
        public int ActionProductId { get; set; }

        [Column("action_id")]
        public int ActionId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("discount_percentage")] // Важливе перейменування
        public decimal Discount { get; set; }

        [ForeignKey("ActionId")]
        public ActionEntity? Action { get; set; }

        [ForeignKey("ProductId")]
        public ProductEntity? Product { get; set; }
    }
}