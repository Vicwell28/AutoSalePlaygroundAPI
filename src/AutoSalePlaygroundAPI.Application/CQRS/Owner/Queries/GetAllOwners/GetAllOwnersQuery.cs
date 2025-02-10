using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetAllOwners
{
    public record GetAllOwnersQuery() 
        : IQuery<ResponseDto<List<OwnerDto>>>;
}
