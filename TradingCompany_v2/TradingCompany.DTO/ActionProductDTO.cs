using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DTO
{
    public class ActionProductDTO
    {
        public int ActionProductId { get; set; }
        public int ActionId { get; set; }
        public int ProductId { get; set; }
        public decimal Discount { get; set; }
        public List<ActionDTO> Action { get; set; } = new List<ActionDTO>();
        public List<ProductDTO> Product { get; set; } = new List<ProductDTO>();
    }
}
