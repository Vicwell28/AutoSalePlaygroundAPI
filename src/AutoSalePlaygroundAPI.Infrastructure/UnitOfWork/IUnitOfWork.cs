namespace AutoSalePlaygroundAPI.Infrastructure
{
    /// <summary>
    /// Define las operaciones de manejo de transacciones para asegurar la integridad de las operaciones
    /// realizadas sobre la base de datos.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Inicia una nueva transacción asíncrona.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirma la transacción actual, guardando los cambios realizados en la base de datos.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Revierte la transacción actual, deshaciendo los cambios realizados en la base de datos.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
