using Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Shared.DataTransferObjects.Error;
using System.Net;

namespace CompanyEmployees.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionFeature is not null)
                {
                    logger.LogError($"Something went wrong: {exceptionFeature.Error}");
                    await context.Response.WriteAsJsonAsync(new ErrorDto()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = exceptionFeature.Error.Message
                    });
                }
            });
        });
    }
}
