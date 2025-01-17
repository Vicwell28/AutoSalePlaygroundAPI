using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoSalePlaygroundAPI.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutoSalePlaygroundAPIDbContext _dbContext;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(AutoSalePlaygroundAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _currentTransaction ??= await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

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
