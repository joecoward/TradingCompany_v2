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
    public class StatusDal : IStatusDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        public StatusDal(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public StatusDTO Create(StatusDTO status)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<StatusDTO> GetAll()
        {
            using(var context = new TradingCompanyContext(_connectionString))
            {
                var statuses = context.Statuses.ToList();
                return _mapper.Map<List<StatusDTO>>(statuses);
            }
        }

        public StatusDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public StatusDTO Update(StatusDTO status)
        {
            throw new NotImplementedException();
        }
    }
}
