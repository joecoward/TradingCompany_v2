using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.Entity;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DALEF.Concrete
{
    public class ProductDal : IProductDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        public ProductDal(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public ProductDTO Create(ProductDTO product)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var entity = new ProductEntity
                {
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.Category.CategoryId
                };
                context.Products.Add(entity);
                context.SaveChanges();
                return _mapper.Map<ProductDTO>(entity);
            }
        }

        public void Delete(int id)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var entity = context.Products.FirstOrDefault(p => p.ProductId == id);
                context.Products.Remove(entity);
                context.SaveChanges();
            }
        }

        public List<ProductDTO> GetAll()
        {
           using (var context = new TradingCompanyContext(_connectionString))
           {
                var entities = context.
                    Products.
                    Include(p=>p.Category).
                    ToList();
                return _mapper.Map<List<ProductDTO>>(entities);
            }
        }

        public ProductDTO GetById(int id)
        {
           using (var context = new TradingCompanyContext(_connectionString))
           {
                var entity = context.
                    Products.
                    Include(p=>p.Category).
                    FirstOrDefault(p => p.ProductId == id);
                return _mapper.Map<ProductDTO>(entity);
            }
        }

        public ProductDTO Update(ProductDTO product)
        {
           using (var context = new TradingCompanyContext(_connectionString))
           {
                var entity = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (entity == null)
                {
                    var newproduct = Create(product);
                    return newproduct;
                }
                entity.Name = product.Name;
                entity.Price = product.Price;
                entity.CategoryId = product.Category.CategoryId;
                context.SaveChanges();
                return _mapper.Map<ProductDTO>(entity);
            }
        }
    }
}
