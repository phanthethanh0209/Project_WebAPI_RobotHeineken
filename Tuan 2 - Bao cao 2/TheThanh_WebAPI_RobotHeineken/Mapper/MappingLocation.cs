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

        }
    }
}
