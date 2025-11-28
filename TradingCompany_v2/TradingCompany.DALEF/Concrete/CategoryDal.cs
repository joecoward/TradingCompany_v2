using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DALEF.Concrete
{
    public class CategoryDal : ICategoryDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        public CategoryDal(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public CategoryDTO Create(CategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> GetAll()
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var categoryEntities = context.Categories.ToList();
                var categoryDtos = _mapper.Map<List<CategoryDTO>>(categoryEntities);
                return categoryDtos;
            }
        }

        public CategoryDTO GetById(int id)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var categoryEntity = context.Categories.FirstOrDefault(c=>c.CategoryId==id);
                if (categoryEntity == null)
                {
                    return null;
                }
                var categoryDto = _mapper.Map<CategoryDTO>(categoryEntity);
                return categoryDto;
            }
        }

        public CategoryDTO Update(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
