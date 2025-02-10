using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAllAccessories
{
    public record GetAllAccessoriesQuery() 
        : IQuery<ResponseDto<List<AccessoryDto>>>;
}
