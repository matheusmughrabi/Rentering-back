using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Rentering.WebAPI.Exceptions;

namespace Rentering.WebAPI.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static IApplicationBuilder UseNativeGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    // Log exception and/or run some other necessary code...

                    var errorResponse = new ErrorResponse();

                    if (exception is HttpException httpException)
                    {
                        errorResponse.StatusCode = httpException.StatusCode;
                        errorResponse.Message = httpException.Message;
                    }

                    context.Response.StatusCode = (int)errorResponse.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorResponse.ToJsonString());
                });
            });

            return app;
        }
    }
}
