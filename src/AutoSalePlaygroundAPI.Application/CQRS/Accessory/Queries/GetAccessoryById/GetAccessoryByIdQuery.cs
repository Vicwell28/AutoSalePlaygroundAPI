using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAccessoryById
{
    public class GetAccessoryByIdQuery : IQuery<ResponseDto<AccessoryDto>>
    {
        public int AccessoryId { get; }

        public GetAccessoryByIdQuery(int accessoryId)
        {
            AccessoryId = accessoryId;
        }
    }
}