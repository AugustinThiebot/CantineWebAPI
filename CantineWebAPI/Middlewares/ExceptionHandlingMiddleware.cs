using Cantine.Application.Errors;
using System.Net;
using System.Text.Json;

namespace CantineWebAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex) {
            var response = context.Response;
            response.ContentType = "application/json";
            var statusCode = ex switch
            {
                ClientNotFoundException => HttpStatusCode.NotFound,
                BudgetTooLowException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            response.StatusCode = (int)statusCode;
            var result = JsonSerializer.Serialize(new
            {
                error = ex.Message,
                statusCode = response.StatusCode
            });
            return response.WriteAsync(result);
        }

    }
}
