using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KubernetesExampleWebApi
{
    /// <summary>
    /// See https://blogs.msdn.microsoft.com/brandonh/2017/07/31/using-middleware-to-trap-exceptions-in-asp-net-core/
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private const string ContentTypeApplicationJson = "application/json";

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented, ContractResolver = new CamelCasePropertyNamesContractResolver() };
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            //Check.NotNull(next, nameof(next));
            //Check.NotNull(loggerFactory, nameof(loggerFactory));

            _next = next;
            _logger = loggerFactory.CreateLogger<CustomExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (AggregateException ae)
            {
                // TODO: refactor this handling code...
                if (ae.InnerException is ArgumentException)
                {
                    await ReturnErrorMessageAsync(context, ae.InnerException, StatusCodes.Status400BadRequest);
                    return;
                }

                throw ae.InnerException;
            }
            catch (ArgumentException ex)
            {
                await ReturnErrorMessageAsync(context, ex, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                await ReturnErrorMessageAsync(context, ex, StatusCodes.Status422UnprocessableEntity);
            }
        }

        private async Task ReturnErrorMessageAsync(HttpContext context, Exception ex, int statusCode)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = ContentTypeApplicationJson;

            var errorModel = new
            {
                ExceptionType = ex.GetType().FullName,
                Message = Regex.Replace(ex.Message, @"\t|[\n\r]", " "),
                Exception = Regex.Replace(ex.ToString(), @"\t|[\n\r]", " ")
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorModel, JsonSerializerSettings)).ConfigureAwait(false);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
