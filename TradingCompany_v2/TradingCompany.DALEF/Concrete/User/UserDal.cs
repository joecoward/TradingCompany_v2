using AutoMapper;
using System.Security.Cryptography;
using TradingCompany.DALEF.Concrete.Context;
using TradingCompany.DALEF.Entity.User;
using TradingCompany.DALEF.interfaces.User;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.Concrete.User
{
    public class UserDal : IUserDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        public UserDal(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public UserDTO Create(UserDTO user, string password)
        {
            using(var context = new TradingCompanyContext(_connectionString))
            {
                Guid saltGuid = Guid.NewGuid();
                var saltBytes = saltGuid.ToByteArray();

                var entity = new UserEntity
                {
                    Login = user.Login,
                    Email = user.Email,
                    Password = HashPassword(password, saltBytes),
                    Salt= saltBytes,
                };
                context.Users.Add(entity);
                context.SaveChanges();
                user.UserId = entity.UserId;
                return user;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserDTO Update(UserDTO user)
        {
            throw new NotImplementedException();
        }
        public byte[] HashPassword(string password, byte[] salt)
        {
           var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

           var hashAlgorithm = HashAlgorithmName.SHA512;
           var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 10000, hashAlgorithm);
           return pbkdf2.GetBytes(64);
        }

        public UserDTO GetUserByLogin(string login)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var userEntity = context.Users.FirstOrDefault(u => u.Login == login);
                if (userEntity == null)
                {
                    return null;
                }
                var userDto = _mapper.Map<UserDTO>(userEntity);
                return userDto;
            }
        }

        public bool ValidateUser(string login, string password)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var userEntity = context.Users.FirstOrDefault(u => u.Login == login);
                if (userEntity == null)
                {
                    return false;
                }
                var hashedPassword = HashPassword(password, userEntity.Salt);
                return hashedPassword.SequenceEqual(userEntity.Password);
            }
        }

        public UserDTO GetUserByEmail(string email)
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                var userEntity = context.Users.FirstOrDefault(u => u.Email == email);
                if (userEntity == null)
                {
                    return null;
                }
                var userDto = _mapper.Map<UserDTO>(userEntity);
                return userDto;
            }
        }
    }
}
