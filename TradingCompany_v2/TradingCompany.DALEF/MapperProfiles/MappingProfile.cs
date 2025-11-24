using AutoMapper;
using TradingCompany.DALEF.Entity;
using TradingCompany.DALEF.Entity.User;
using TradingCompany.DTO;
using TradingCompany.DTO.User;

namespace TradingCompany.DALEF.MapperProfiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<RoleEntity, RoleDTO>().ReverseMap();
            CreateMap<UserRoleEntity, UserRoleDTO>().ReverseMap();

            CreateMap<ActionEntity, ActionDTO>().ReverseMap();
            CreateMap<ProductEntity, ProductDTO>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
            CreateMap<StatusEntity, StatusDTO>().ReverseMap();
            CreateMap<ActionProductEntity, ActionProductDTO>().ReverseMap();

        }
    }
}
