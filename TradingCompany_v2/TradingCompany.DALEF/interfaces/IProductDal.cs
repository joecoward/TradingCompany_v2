using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;

namespace TradingCompany.DALEF.interfaces
{
    public interface IProductDal
    {
        public ProductDTO Create(ProductDTO product);
        public ProductDTO GetById(int id);
        public List<ProductDTO> GetAll();
        public ProductDTO Update(ProductDTO product);
        public void Delete(int id);
    }
}
