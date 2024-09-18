using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Mapper
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<CreateUserDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
