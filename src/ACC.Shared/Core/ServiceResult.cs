namespace ACC.Shared.Core
{
    //-----------* PURA ELEGANCIA DE CODIGO SEÑORESSSS *----------------//
    // clase para manejar tipos de errores:

    public static class HttpStatusCodes // codigos de errores
    {
        public const int OK = 200; // resultado exitoso

        public const int BadRequest = 400; // mala solicitud

        public const int Unauthorized = 401; // no autorizado

        public const int Forbidden = 403;// prohibido

        public const int NotFound = 404;// no encontrado

        public const int InternalServerError = 500;// error interno del servidor
    }

    // Clase genérica para manejar los resultados de los servicios que regresan un valor
    public class ServiceResult<T>
    {
        public bool Success { get; set; } // Indica si la operación fue exitosa
        public string? Message { get; set; } // Mensaje opcional con información adicional
        public T? Data { get; set; } // Datos resultantes de la operación
        public int? StatusCode { get; set; } // Código de estado HTTP opcional

        // Método estático para crear un resultado exitoso
        public static ServiceResult<T> Ok(T? data, string? message = null)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = HttpStatusCodes.OK
            };
        }

        // Método estático para crear un resultado fallido
        public static ServiceResult<T> Fail(string message, int? statusCode = 400)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.BadRequest
            };
        }

        // Método estático para capturar excepciones y crear un resultado fallido
        public static ServiceResult<T> Error(Exception ex)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = $"An error occurred: {ex.Message}",
                StatusCode = HttpStatusCodes.InternalServerError
            };
        }

        // Método estático para manejar errores específicos
        public static ServiceResult<T> NotFound(string message = "Resource not found")
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.NotFound
            };
        }
        // metodo para manejar errores de autorizacion
        public static ServiceResult<T> Unauthorized(string message = "Unauthorized access")
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.Unauthorized
            };
        }
        // metodo para manejar errores de no acceso
        public static ServiceResult<T> Forbidden(string message = "Forbidden access")
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.Forbidden
            };
        }
    }

    // Clase para manejar los resultados de los servicios que no regresan un valor
    public class ServiceResult
    {
        public bool Success { get; set; } // Indica si la operación fue exitosa
        public string? Message { get; set; } // Mensaje opcional con información adicional
        public int? StatusCode { get; set; } // Código de estado HTTP opcional

        // Método estático para crear un resultado exitoso
        public static ServiceResult Ok(string? message = null)
        {
            return new ServiceResult
            {
                Success = true,
                Message = message,
                StatusCode = HttpStatusCodes.OK
            };
        }

        // Método estático para crear un resultado fallido
        public static ServiceResult Fail(string message)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.BadRequest
            };
        }

        // Método estático para capturar excepciones y crear un resultado fallido
        public static ServiceResult Error(Exception ex)
        {
            return new ServiceResult
            {
                Success = false,
                Message = $"An error occurred: {ex.Message}",
                StatusCode = HttpStatusCodes.InternalServerError
            };
        }

        // Método estático para manejar errores específicos
        public static ServiceResult NotFound(string message = "Resource not found")
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.NotFound
            };
        }

        // metodo para manejar errores de autorizacion
        public static ServiceResult Unauthorized(string message = "Unauthorized access")
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.Unauthorized
            };
        }

        // metodo para manejar errores de no acceso.
        public static ServiceResult Forbidden(string message = "Forbidden access")
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
                StatusCode = HttpStatusCodes.Forbidden
            };
        }
    }
}
