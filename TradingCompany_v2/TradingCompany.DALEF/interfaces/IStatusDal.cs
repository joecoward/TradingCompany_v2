using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DALEF.interfaces
{
    public interface IStatusDal
    {
        public StatusDTO Create(StatusDTO status);
        public StatusDTO GetById(int id);
        public List<StatusDTO> GetAll();
        public StatusDTO Update(StatusDTO status);
        public void Delete(int id);
    }
}
