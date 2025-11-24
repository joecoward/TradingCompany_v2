using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.Entity;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DALEF.Concrete
{

    public class ActionDal : IActionDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public ActionDal(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public ActionDTO Create(ActionDTO action)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var newEntity = new ActionEntity
                {
                    Name = action.Name,
                    Description = action.Description,
                    StartDate = action.StartDate,
                    EndDate = action.EndDate,
                    Status = _mapper.Map<StatusEntity>(action.Status)
                };

                context.Actions.Add(newEntity);
                context.SaveChanges();
                action.ActionId = newEntity.ActionId;
                return action;
            }
        }

        public void Delete(int id)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    context.Actions.Remove(_mapper.Map<ActionEntity>(entity));
                    context.SaveChanges();
                }
            }
        }

        public List<ActionDTO> GetAll()
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
             var entities = context.Actions
            .Include(x => x.Status)
            .ToList();
                return _mapper.Map<List<ActionDTO>>(entities);
            }
        }

        public ActionDTO GetById(int id)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var entity = context.Actions.FirstOrDefault(a => a.ActionId == id);
                return _mapper.Map<ActionDTO>(entity);
            }
        }

        public ActionDTO Update(ActionDTO action)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var entity = GetById(action.ActionId);
                if (entity != null)
                {
                    entity.Name = action.Name;
                    entity.Description = action.Description;
                    entity.StartDate = action.StartDate;
                    entity.EndDate = action.EndDate;
                    entity.Status = _mapper.Map<StatusDTO>(action.Status);
                    context.SaveChanges();
                }
                return action;
            }
        }
    }
}
