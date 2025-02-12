namespace AutoSalePlaygroundAPI.Domain.DTOs.Response
{
    /// <summary>
    /// Representa una respuesta paginada, que incluye la data paginada y la información de paginación.
    /// Hereda de <see cref="ResponseDto{T}"/>, donde <c>T</c> es una colección de elementos.
    /// </summary>
    /// <typeparam name="T">El tipo de elementos contenidos en la respuesta.</typeparam>
    public class PaginatedResponseDto<T> : ResponseDto<IEnumerable<T>>
    {
        /// <summary>
        /// Página actual de la respuesta.
        /// </summary>
        public int CurrentPage { get; protected set; }

        /// <summary>
        /// Tamaño de página (cantidad de elementos por página).
        /// </summary>
        public int PageSize { get; protected set; }

        /// <summary>
        /// Número total de elementos.
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// Número total de páginas, calculado en base a <see cref="TotalCount"/> y <see cref="PageSize"/>.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// Indica si existe una página anterior.
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Indica si existe una página siguiente.
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Establece los datos de paginación de la respuesta.
        /// </summary>
        /// <param name="currentPage">Página actual.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        /// <param name="totalCount">Número total de elementos.</param>
        public void SetPaginationData(int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Crea una respuesta exitosa paginada con la data y los datos de paginación especificados.
        /// </summary>
        /// <param name="data">La colección de elementos.</param>
        /// <param name="currentPage">Página actual.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        /// <param name="totalCount">Número total de elementos.</param>
        /// <param name="message">Mensaje opcional.</param>
        /// <param name="code">Código opcional.</param>
        /// <returns>Una instancia de <see cref="PaginatedResponseDto{T}"/> con la respuesta exitosa.</returns>
        public static PaginatedResponseDto<T> Success(IEnumerable<T> data, int currentPage, int pageSize, int totalCount, string? message = null, string? code = null)
        {
            var response = new PaginatedResponseDto<T>
            {
                Data = data,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            response.SetSuccess(message, code);
            return response;
        }

        /// <summary>
        /// Crea una respuesta de error paginada.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="errors">Lista de errores.</param>
        /// <param name="code">Código opcional de error.</param>
        /// <returns>Una instancia de <see cref="PaginatedResponseDto{T}"/> con la respuesta de error.</returns>
        public new static PaginatedResponseDto<T> Error(string message, List<string> errors, string? code = null)
        {
            var response = new PaginatedResponseDto<T>();
            response.SetError(message, errors, code);
            return response;
        }
    }
}
