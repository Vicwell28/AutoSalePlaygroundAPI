using MediatR;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    public interface IRequireValidation { }
    public interface IRequireAuthentication { }
    public interface IRequireAuthorization { }
    public interface IQuery<TResponse> : IRequest<TResponse> { }
    public interface ICommand<TResponse> : IRequest<TResponse> { }
}
