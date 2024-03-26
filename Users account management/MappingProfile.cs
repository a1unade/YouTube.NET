using AutoMapper;
using Users_account_management.Domain_Models;
using Users_account_management.DTO;

namespace Users_account_management
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
