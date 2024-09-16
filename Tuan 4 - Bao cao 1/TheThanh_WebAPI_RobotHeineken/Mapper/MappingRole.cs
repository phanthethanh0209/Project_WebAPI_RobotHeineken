﻿using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;

namespace TheThanh_WebAPI_RobotHeineken.Mapper
{
    public class MappingRole : Profile
    {
        public MappingRole()
        {
            CreateMap<RoleDTO, Role>().ReverseMap();
        }
    }
}
