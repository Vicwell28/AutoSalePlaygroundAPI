namespace AutoSalePlaygroundAPI.Application.DTOs.Response
{
    public class ResponseDto<T> : BaseResponseDto
    {
        public T? Data { get; protected set; }

        public virtual void SetSuccess(T data, string? message = null, string? code = null)
        {
            base.SetSuccess(message, code);

            Data = data;
        }

        //Cracion de metodos estaticos para su respuesta 
        public static ResponseDto<T> Success(T data, string? message = null, string? code = null)
        {
            var response = new ResponseDto<T>
            {
                Data = data
            };
            response.SetSuccess(data, message, code);
            return response;
        }

        public static ResponseDto<T> Error(string message, List<string> errors, string? code = null)
        {
            var response = new ResponseDto<T>();
            response.SetError(message, errors, code);
            return response;
        }
    }
}