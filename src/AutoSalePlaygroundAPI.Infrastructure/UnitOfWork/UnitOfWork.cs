using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoSalePlaygroundAPI.Infrastructure
{
    /// <summary>
    /// Implementa el patrón Unit of Work para gestionar transacciones de base de datos en AutoSalePlaygroundAPIDbContext.
    /// </summary>
    public class UnitOfWork(AutoSalePlaygroundAPIDbContext dbContext) : IUnitOfWork
    {
        private readonly AutoSalePlaygroundAPIDbContext _dbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext));

        // Representa la transacción de base de datos actual.
        private IDbContextTransaction? _currentTransaction;

        /// <inheritdoc />
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            // Si no existe una transacción activa, inicia una nueva.
            _currentTransaction ??= await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                // Guarda todos los cambios pendientes en el contexto.
                await _dbContext.SaveChangesAsync(cancellationToken);
                // Confirma la transacción actual.
                await _currentTransaction.CommitAsync(cancellationToken);
                // Libera los recursos asociados a la transacción.
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        /// <inheritdoc />
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                // Revierte la transacción actual, deshaciendo los cambios.
                await _currentTransaction.RollbackAsync(cancellationToken);
                // Libera los recursos asociados a la transacción.
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}
