namespace AutoSalePlaygroundAPI.Domain.DTOs.Response
{
    /// <summary>
    /// Representa una respuesta que incluye datos de tipo <typeparamref name="T"/> junto con información de estado, mensaje y errores.
    /// Hereda de <see cref="BaseResponseDto"/>.
    /// </summary>
    /// <typeparam name="T">El tipo de dato contenido en la respuesta.</typeparam>
    public class ResponseDto<T> : BaseResponseDto
    {
        /// <summary>
        /// Obtiene los datos de la respuesta.
        /// </summary>
        public T? Data { get; protected set; }

        /// <summary>
        /// Establece la respuesta como exitosa y asigna los datos correspondientes.
        /// </summary>
        /// <param name="data">Los datos de la respuesta.</param>
        /// <param name="message">Mensaje opcional.</param>
        /// <param name="code">Código opcional.</param>
        public virtual void SetSuccess(T data, string? message = null, string? code = null)
        {
            base.SetSuccess(message, code);
            Data = data;
        }

        /// <summary>
        /// Crea una respuesta exitosa con los datos y la información opcional especificada.
        /// </summary>
        /// <param name="data">Los datos de la respuesta.</param>
        /// <param name="message">Mensaje opcional.</param>
        /// <param name="code">Código opcional.</param>
        /// <returns>Una instancia de <see cref="ResponseDto{T}"/> con la respuesta exitosa.</returns>
        public static ResponseDto<T> Success(T data, string? message = null, string? code = null)
        {
            var response = new ResponseDto<T>
            {
                Data = data
            };
            response.SetSuccess(data, message, code);
            return response;
        }

        /// <summary>
        /// Crea una respuesta de error con el mensaje y la lista de errores especificados.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="errors">Lista de errores.</param>
        /// <param name="code">Código opcional de error.</param>
        /// <returns>Una instancia de <see cref="ResponseDto{T}"/> con la respuesta de error.</returns>
        public static ResponseDto<T> Error(string message, List<string> errors, string? code = null)
        {
            var response = new ResponseDto<T>();
            response.SetError(message, errors, code);
            return response;
        }
    }
}
