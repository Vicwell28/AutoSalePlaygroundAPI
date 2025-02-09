using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.ValueObjects;

namespace AutoSalePlaygroundAPI.Application.Mappings
{
    public class SpecificationsProfile : Profile
    {
        public SpecificationsProfile()
        {
            CreateMap<Specifications, SpecificationsDto>().ReverseMap();
        }
    }
}