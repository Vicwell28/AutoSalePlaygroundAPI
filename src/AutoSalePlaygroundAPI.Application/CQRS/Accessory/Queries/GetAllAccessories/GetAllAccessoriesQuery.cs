using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAllAccessories
{
    public class GetAllAccessoriesQuery : IQuery<ResponseDto<List<AccessoryDto>>> { }
}