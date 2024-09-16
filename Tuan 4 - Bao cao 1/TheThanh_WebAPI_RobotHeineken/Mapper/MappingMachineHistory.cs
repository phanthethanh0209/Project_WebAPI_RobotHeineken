using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Mapper
{
    public class MappingMachineHistory : Profile
    {
        public MappingMachineHistory()
        {
            CreateMap<RecyclingHistoryDTO, RecyclingHistory>().ReverseMap();
        }
    }
}
