using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Domain.Entities;

namespace AutoSalePlaygroundAPI.Application.Mappings
{
    public class AccessoryProfile : Profile
    {
        public AccessoryProfile()
        {
            CreateMap<Accessory, AccessoryDto>().ReverseMap();
        }
    }
}
