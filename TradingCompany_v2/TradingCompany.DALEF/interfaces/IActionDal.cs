using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.interfaces
{
    public interface IActionDal
    {
        public ActionDTO Create(ActionDTO action);
        public ActionDTO GetById(int id);
        public List<ActionDTO> GetAll();
        public ActionDTO Update(ActionDTO action);
        public void Delete(int id);
    }
}
