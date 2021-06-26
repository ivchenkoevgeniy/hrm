using System;
using AutoMapper;
using HRM.DataAccessEf.Entities;
using HRM.Domain.DTO;

namespace HRM.Domain.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.IsFullTime, opt => opt.MapFrom(src => src.IsFullTime))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateTime.Parse(src.Birthday)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.IsFullTime, opt => opt.MapFrom(src => src.IsFullTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}