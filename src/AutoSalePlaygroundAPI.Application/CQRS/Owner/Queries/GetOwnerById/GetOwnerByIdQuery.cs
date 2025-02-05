using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetOwnerById
{
    public class GetOwnerByIdQuery : IQuery<ResponseDto<OwnerDto>>
    {
        public int OwnerId { get; }

        public GetOwnerByIdQuery(int ownerId)
        {
            OwnerId = ownerId;
        }
    }
}