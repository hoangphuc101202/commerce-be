using Microsoft.AspNetCore.Mvc;
using Restapi_net8.Exceptions;
using Serilog;
using System.Net;
using Newtonsoft.Json;

namespace Restapi_net8.Middlewares
{
    public sealed class HttpExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (HttpException exception)
            {
                await HandleExceptionAsync(
                    context, GenerateProblemDetails(context, exception.StatusCode, exception.Message), exception);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(
                    context, GenerateProblemDetails(context, HttpStatusCode.InternalServerError, exception.Message), exception);
            }
        }

        private static ProblemDetails GenerateProblemDetails(HttpContext context, HttpStatusCode code, string message)
        {
            // Machine-readable format for specifying errors in HTTP API responses.
            // Based on https://tools.ietf.org/html/rfc7807.
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails
            return new ProblemDetails
            {
                Status = (int)code,
                Title = message,
                Instance = context.Request.Path
            };
        }

        private static Task HandleExceptionAsync(HttpContext context, ProblemDetails problemDetails, Exception exception)
        {
            // Check if the response status, reason and headers can be modified.
            // If HasStarted is true, they can't.
            if (context.Response.HasStarted)
            {
                return Task.CompletedTask;
            }

            if (Equals(StatusCodes.Status500InternalServerError, problemDetails.Status))
            {
                Log.Warning($"(HTTP exception {problemDetails.Status}) {problemDetails.Title}");
            }
            else
            {
                Log.Information($"(HTTP exception {problemDetails.Status}) {problemDetails.Title}");
            }

            Log.Debug($"Exception details => {JsonConvert.SerializeObject(exception)}");

            context.Response.Clear();
            context.Response.ContentType = "application/problem+json";

            if (problemDetails.Status != null)
            {
                context.Response.StatusCode = (int)problemDetails.Status;
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }
    }
}
