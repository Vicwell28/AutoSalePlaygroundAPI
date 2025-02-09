namespace AutoSalePlaygroundAPI.Domain.DTOs.Response
{
    public class BaseResponseDto
    {
        public bool IsSuccess => Errors == null || !Errors.Any();
        public string? Message { get; protected set; }
        public List<string>? Errors { get; protected set; }
        public string? Code { get; protected set; }

        public virtual void SetSuccess(string? message = null, string? code = null)
        {
            Message = message;
            Errors = null;
            Code = code;
        }

        public virtual void SetError(string? message, List<string>? errors = null, string? code = null)
        {
            Message = message;
            Errors = errors ?? new List<string>();
            Code = code;
        }
    }
}
