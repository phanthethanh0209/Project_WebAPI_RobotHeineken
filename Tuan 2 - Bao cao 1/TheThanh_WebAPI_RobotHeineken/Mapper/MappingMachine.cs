using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Mapper
{
    public class MappingMachine : Profile
    {
        public MappingMachine()
        {
            CreateMap<CreateMachineDTO, RecyclingMachine>().ReverseMap();

            CreateMap<UpdateMachineDTO, RecyclingMachine>() // update
                 .ForMember(dest => dest.CreateAt, opt => opt.Ignore()); // Bỏ qua CreateAt khi map;

            CreateMap<RecyclingMachine, MachineDTO>() // getAll
            .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status == 1 ? "Online" : "Offline"))
            .ForMember(dest => dest.ContainerStatus,
                    opt => opt.MapFrom(src => src.ContainerStatus == 1 ? "Sắp đầy" : "Chưa đầy"))
            .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src => src.Location != null ? src.Location.City : "Không xác định"));


        }
    }
}
