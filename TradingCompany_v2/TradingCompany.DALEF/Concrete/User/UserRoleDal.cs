using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.Entity;
using TradingCompany.DALEF.interfaces.User;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.Concrete.User
{
    public class UserRoleDal : IUserRoleDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        public UserRoleDal(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public UserRoleDTO Create(UserDTO userDto)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var entity = new UserRoleEntity
                {
                    RoleId = 1,
                    UserId = userDto.UserId
                };
                context.UserRoles.Add(entity);
                context.SaveChanges();
                return _mapper.Map<UserRoleDTO>(entity);

            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserRoleDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserRoleDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserRoleDTO Update(UserRoleDTO userRole)
        {
            throw new NotImplementedException();
        }
    }
}
