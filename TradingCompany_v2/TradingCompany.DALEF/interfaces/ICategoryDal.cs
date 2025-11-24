using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DALEF.interfaces
{
    public interface ICategoryDal
    {
        public CategoryDTO Create(CategoryDTO category);
        public CategoryDTO GetById(int id);
        public List<CategoryDTO> GetAll();
        public CategoryDTO Update(CategoryDTO category);
        public void Delete(int id);
    }
}
