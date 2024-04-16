using Contracts;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Shared.DataTransferObjects.Error;

namespace CompanyEmployees.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionFeature is not null)
                {
                    context.Response.StatusCode = exceptionFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

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
