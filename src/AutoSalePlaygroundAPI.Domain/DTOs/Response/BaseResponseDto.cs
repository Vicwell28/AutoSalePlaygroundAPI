namespace AutoSalePlaygroundAPI.Domain.DTOs.Response
{
    /// <summary>
    /// Representa la respuesta base de un DTO, que incluye información sobre el éxito de la operación,
    /// mensajes, códigos y, en caso de error, la lista de errores.
    /// </summary>
    public class BaseResponseDto : IDto
    {
        /// <summary>
        /// Indica si la respuesta es exitosa.
        /// Es <c>true</c> cuando la propiedad <see cref="Errors"/> es nula o está vacía.
        /// </summary>
        public bool IsSuccess => Errors == null || !Errors.Any();

        /// <summary>
        /// Mensaje adicional de la respuesta.
        /// </summary>
        public string? Message { get; protected set; }

        /// <summary>
        /// Lista de errores ocurridos durante la operación.
        /// </summary>
        public List<string>? Errors { get; protected set; }

        /// <summary>
        /// Código asociado a la respuesta (por ejemplo, código de error o de estado).
        /// </summary>
        public string? Code { get; protected set; }

        /// <summary>
        /// Establece la respuesta como exitosa, asignando opcionalmente un mensaje y un código.
        /// </summary>
        /// <param name="message">Mensaje de éxito.</param>
        /// <param name="code">Código asociado.</param>
        public virtual void SetSuccess(string? message = null, string? code = null)
        {
            Message = message;
            Errors = null;
            Code = code;
        }

        /// <summary>
        /// Establece la respuesta como fallida, asignando un mensaje, una lista de errores y un código.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="errors">Lista de errores (si es nula, se inicializa una lista vacía).</param>
        /// <param name="code">Código de error asociado.</param>
        public virtual void SetError(string? message, List<string>? errors = null, string? code = null)
        {
            Message = message;
            Errors = errors ?? new List<string>();
            Code = code;
        }
    }
}
