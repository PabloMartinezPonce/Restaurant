using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Web.Common;
using Restaurante.Data.Resources;
using Restaurante.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Controller> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<Controller> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AccessViolationException avEx)
            {
                _logger.LogError(ResxResponses.logMiddlewareAccessViolation + avEx);
                await HandleExceptionAsync(httpContext, avEx);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(string.Format(ResxResponses.log500NotControledException, DateTime.UtcNow.ToLongTimeString(), ResxResponses.productGetByIdMethod, ex.Message));
                await HandleExceptionAsync(httpContext, ex);
            }
            //catch (FluentValidation.ValidationException ex)
            //{
            //    _logger.LogError(string.Format(ResxResponses.log400BadRequest, DateTime.UtcNow.ToLongTimeString(), ResxResponses.productGetByIdMethod, ex.Message));
            //    await HandleExceptionAsync(httpContext, ex);
            //}
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(string.Format(ResxResponses.log404NotFound, DateTime.UtcNow.ToLongTimeString(), ResxResponses.productGetByIdMethod, ex.Message));
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ResxResponses.logMiddlewareException + ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = ResxResponses.contentTypeJson;

            var statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                AccessViolationException => (int)HttpStatusCode.InternalServerError,
                InvalidOperationException => (int)HttpStatusCode.NotFound,
                //FluentValidation.ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            var message = exception switch
            {
                KeyNotFoundException => ResxResponses.log404NotFound,
                AccessViolationException => ResxResponses.middlewareCusomAccessViolation,
                InvalidOperationException => ResxResponses.middlewareCusomException,
                //FluentValidation.ValidationException => ResxResponses.log400BadRequest + exception.Message,
                _ => $"{ResxResponses.middlewareCusomException} {CommonTxt.GetMessage(exception)}" 
            };

            context.Response.StatusCode = statusCode; // Establece el código de estado HTTP
            await context.Response.WriteAsync(message);
        }
    }
}
