using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Mapper
{
    public class MappingLocation : Profile
    {
        public MappingLocation()
        {
            CreateMap<Location, LocationDTO>().ReverseMap(); // ánh xạ đối tượng Location qua LocationDTO
            CreateMap<RecyclingMachine, LocationMachineDTO>().
                ForMember(dest => dest.Status,
                            opt => opt.MapFrom(src => src.Status == 1 ? "Online" : "Offline"));
        }
    }
}
