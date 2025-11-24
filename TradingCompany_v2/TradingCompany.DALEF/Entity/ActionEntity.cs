using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradingCompany.DALEF.Entity
{
    [Table("Actions")]
    public class ActionEntity
    {
        [Key]
        [Column("action_id")]
        public int ActionId { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public StatusEntity? Status { get; set; }

        public ICollection<ActionProductEntity> ActionProducts { get; set; } = new List<ActionProductEntity>();
    }
}