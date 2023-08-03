using Couple.Budget.Core.Exceptions;
using Couple.Budget.Host.ApiResponses;
using System.Net;
using System.Text.Json;

namespace Couple.Budget.Host.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private const int DEFAULT_STATUS_CODE = 400;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ApiResponse response = null;
            int statusCode = DEFAULT_STATUS_CODE;

            try
            {
                await next(context);
                return;
            }
            catch(ValidationException e)
            {
                response = ApiResponse.Error(e.ValidationFailuresMessages);
            }
            catch (Exception e)
            {
                var exceptionsMessages = GetExceptionMessages(e);
                var exceptionsStackTrace = e.ToString() + (e.InnerException is not null ? Environment.NewLine + e.InnerException.ToString() : "");
                response = ApiResponse.Error(exceptionsMessages, exceptionsStackTrace);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        private static IEnumerable<string> GetExceptionMessages(Exception e)
        {
            var exceptionMessages = new List<string>();

            exceptionMessages.Add(e.Message);

            if (e.InnerException is not null)
            {
                exceptionMessages.Add(e.InnerException.Message);
            }

            return exceptionMessages;
        }
    }
}