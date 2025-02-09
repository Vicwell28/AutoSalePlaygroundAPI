using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
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
