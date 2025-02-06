using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                // Inicia la transacción
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                // Ejecuta la lógica del Handler
                var response = await next();

                // Si no ha habido excepciones, se confirma la transacción
                await _unitOfWork.CommitAsync(cancellationToken);

                return response;
            }
            catch
            {
                // Ante cualquier excepción se revierten los cambios
                await _unitOfWork.RollbackAsync(cancellationToken);

                throw;
            }
        }
    }
}
