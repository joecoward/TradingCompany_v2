using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DALEF.interfaces
{
    public interface IActionProductDal
    {
        public ActionProductDTO Create(ActionProductDTO actionProduct);
        public ActionProductDTO GetById(int id);
        public List<ActionProductDTO> GetAll();
        public ActionProductDTO Update(ActionProductDTO actionProduct);
        public void Delete(int id);
    }
}
