using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Mapper
{
    public class MappingQRCode : Profile
    {
        public MappingQRCode()
        {
            CreateMap<QRCodeDTO, QRCode>().ReverseMap();
        }
    }
}
