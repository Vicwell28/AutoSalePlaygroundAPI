using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoSalePlaygroundAPI.Infrastructure
{
    public class UnitOfWork(AutoSalePlaygroundAPIDbContext dbContext) : IUnitOfWork
    {
        private readonly AutoSalePlaygroundAPIDbContext _dbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext));

        private IDbContextTransaction? _currentTransaction;

        /// <inheritdoc />
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _currentTransaction ??= await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _currentTransaction.CommitAsync(cancellationToken);
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        /// <inheritdoc />
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}
