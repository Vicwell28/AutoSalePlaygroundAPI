namespace AutoSalePlaygroundAPI.Application.DTOs.Response
{
    public class PaginatedResponseDto<T> : ResponseDto<IEnumerable<T>>
    {
        public int CurrentPage { get; protected set; }
        public int PageSize { get; protected set; }
        public int TotalCount { get; protected set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public void SetPaginationData(int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

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

        public new static PaginatedResponseDto<T> Error(string message, List<string> errors, string? code = null)
        {
            var response = new PaginatedResponseDto<T>();
            response.SetError(message, errors, code);
            return response;
        }
    }
}