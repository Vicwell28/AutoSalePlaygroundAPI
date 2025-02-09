using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetAllOwners
{
    public class GetAllOwnersQuery : IQuery<ResponseDto<List<OwnerDto>>> { }
}
