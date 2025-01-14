using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Domain.Entities;

namespace AutoSalePlaygroundAPI.Application.Mappings
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            // Mapeo de una supuesta entidad Vehicle -> VehicleDto
            // y viceversa
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
        }
    }
}
