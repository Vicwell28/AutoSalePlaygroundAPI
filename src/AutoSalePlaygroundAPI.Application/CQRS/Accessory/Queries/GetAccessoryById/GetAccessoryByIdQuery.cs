using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAccessoryById
{
    public record GetAccessoryByIdQuery(int AccessoryId) 
        : IQuery<ResponseDto<AccessoryDto>>;
}
