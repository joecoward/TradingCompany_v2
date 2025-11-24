using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingCompany.DALEF.Entity
{
    [Table("Statuses")]
    public class StatusEntity
    {
        [Key]
        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("status")]
        public string? Name { get; set; }

        public ICollection<ActionEntity> Actions { get; set; } = new List<ActionEntity>();
    }
}