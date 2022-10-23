using System;
using AutoMapper;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
            // Employee attributes are different in different layers (client, business)
            // Different names, different types
            // Therefore, I had to map them
            CreateMap<EmployeeInfo, EmployeeDto>()
                .ForMember(dest => dest.OccupationName,
                    opt => opt.MapFrom(src => src.Occupation))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(x => x.Phone))
                .ForMember(dest => dest.LastModifiedDateTime, source => source.MapFrom<DateTime>(x => x.LastModified));
            
            CreateMap<EmployeeDto, EmployeeInfo>()
                .ForMember(dest => dest.Occupation,
                    opt => opt.MapFrom(src => src.OccupationName))
                .ForMember(dest => dest.Phone, source => source.MapFrom(x => x.PhoneNumber))
                .ForMember(dest => dest.LastModified, source => source.MapFrom<string>(x => x.LastModifiedDateTime));
        }
    }
}