using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HandlerException
{
    /// <summary>
    /// Clase genérica que representa el resultado de una solicitud.
    /// Proporciona información sobre el estado de la solicitud, mensajes de error, el resultado de la operación y cualquier excepción que pueda haber ocurrido.
    /// </summary>
    /// <typeparam name="T">Tipo del resultado de la operación.</typeparam>
    public sealed class RequestResult<T>
    {
        /// <summary>
        /// Constructor privado para crear una instancia de RequestResult.
        /// </summary>
        /// <param name="resultType">Tipo de resultado de la solicitud.</param>
        /// <param name="messages">Mensajes asociados con la solicitud.</param>
        /// <param name="result">Resultado de la solicitud.</param>
        /// <param name="exception">Excepción ocurrida durante la solicitud, si la hay.</param>
        private RequestResult(RequestResultType resultType, List<string> messages, T result, Exception exception)
        {
            ResultType = resultType;
            Messages = messages;
            Result = result;
            Exception = exception;
        }

 
        /// Tipo de resultado de la solicitud.
        public RequestResultType ResultType { get; }
        /// Lista de mensajes asociados con la solicitud.
        public List<string> Messages { get; }
        /// Resultado de la solicitud.   
        public T Result { get; }
        /// Excepción ocurrida durante la solicitud, si la hay.
        public Exception Exception { get; }

        /// <summary>
        /// Crea una instancia de RequestResult representando un resultado exitoso.
        /// </summary>
        /// <param name="result">Resultado de la operación.</param>
        /// <returns>Instancia de RequestResult con estado Successful.</returns>
        public static RequestResult<T> CreateSuccessful(T result)
        {
            return new RequestResult<T>(RequestResultType.Successful, null, result, null);
        }

        /// <summary>
        /// Crea una instancia de RequestResult representando un resultado no exitoso con una lista de mensajes.
        /// </summary>
        /// <param name="messages">Lista de mensajes explicando la causa del fallo.</param>
        /// <returns>Instancia de RequestResult con estado Unsuccessful.</returns>
        public static RequestResult<T> CreateUnsuccessful(List<string> messages)
        {
            return new RequestResult<T>(RequestResultType.Unsuccessful, messages, default, null);
        }

        /// <summary>
        /// Crea una instancia de RequestResult representando un resultado no exitoso con un solo mensaje.
        /// </summary>
        /// <param name="message">Mensaje explicando la causa del fallo.</param>
        /// <returns>Instancia de RequestResult con estado Unsuccessful.</returns>
        public static RequestResult<T> CreateUnsuccessful(string message)
        {
            return new RequestResult<T>(RequestResultType.Unsuccessful, new List<string> { message }, default, null);
        }

        /// <summary>
        /// Crea una instancia de RequestResult representando un error con un solo mensaje.
        /// </summary>
        /// <param name="message">Mensaje explicando la causa del error.</param>
        /// <returns>Instancia de RequestResult con estado Error.</returns>
        public static RequestResult<T> CreateError(string message)
        {
            return new RequestResult<T>(RequestResultType.Error, new List<string> { message }, default, null);
        }

        /// <summary>
        /// Crea una instancia de RequestResult representando un error con una lista de mensajes.
        /// </summary>
        /// <param name="messages">Lista de mensajes explicando la causa del error.</param>
        /// <returns>Instancia de RequestResult con estado Error.</returns>
        public static RequestResult<T> CreateError(List<string> messages)
        {
            return new RequestResult<T>(RequestResultType.Error, messages, default, null);
        }

        /// <summary>
        /// Crea una instancia de RequestResult representando un error a partir de una excepción.
        /// </summary>
        /// <param name="exception">Excepción que causó el error.</param>
        /// <returns>Instancia de RequestResult con estado Error.</returns>
        public static RequestResult<T> CreateError(Exception exception)
        {
            List<string> messages = new List<string> { $"Ha ocurrido una excepción inesperada: {exception.Message}" };
            return new RequestResult<T>(RequestResultType.Error, messages, default, exception);
        }
    }

    /// <summary>
    /// Enum que representa los posibles tipos de resultados de una solicitud.
    /// </summary>
    public enum RequestResultType
    {
        /// <summary>
        /// Indica que la solicitud fue exitosa.
        /// </summary>
        Successful,

        /// <summary>
        /// Indica que la solicitud no fue exitosa.
        /// </summary>
        Unsuccessful,

        /// <summary>
        /// Indica que ocurrió un error durante la solicitud.
        /// </summary>
        Error
    }
}
