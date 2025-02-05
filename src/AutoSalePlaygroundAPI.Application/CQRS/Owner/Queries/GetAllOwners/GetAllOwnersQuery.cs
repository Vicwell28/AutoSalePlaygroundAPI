using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetAllOwners
{
    public class GetAllOwnersQuery : IQuery<ResponseDto<List<OwnerDto>>> { }
}
