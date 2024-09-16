using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Mapper
{
    public class MappingGift : Profile
    {
        public MappingGift()
        {
            //CreateMap<GiftDTO, Gift>().ReverseMap();
            CreateMap<Gift, GiftDTO>().ReverseMap();
        }
    }
}
