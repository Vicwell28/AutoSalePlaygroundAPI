using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetOwnerById
{
    public record GetOwnerByIdQuery(int OwnerId) 
        : IQuery<ResponseDto<OwnerDto>>;
}
