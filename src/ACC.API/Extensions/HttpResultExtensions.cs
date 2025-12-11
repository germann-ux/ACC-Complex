using ACC.Shared.Core;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Extensions
{
    public static class HttpResultExtensions
    {
        public static ActionResult ToHttp<T>(this ServiceResult<T> r, ControllerBase c)
        {
            if (r is null) return c.StatusCode(500);

            if (r.Success)
                return r.Data is null ? c.NoContent() : c.Ok(r.Data);

            // Construir ProblemDetails con metadata útil
            var status = r.StatusCode >= 400 && r.StatusCode < 600 ? r.StatusCode : 500;
            var pd = new ProblemDetails
            {
                Title = "Operación fallida",
                Detail = r.Message,
                Status = status
            };
            pd.Extensions["traceId"] = c.HttpContext.TraceIdentifier;

            return c.StatusCode((int)status, pd);
        }
    }
}
