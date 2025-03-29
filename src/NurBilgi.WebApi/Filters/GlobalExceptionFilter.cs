using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NurBilgi.Application.Common.Models.Errors;
using NurBilgi.Application.Common.Models.Responses;
using System.Net;

namespace NurBilgi.WebApi.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        _logger.LogError(exception, exception.Message);

        var errorResponse = new ResponseDto<string>();
        var statusCode = HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case ValidationException validationException:
                var validationErrors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .Select(g => new ValidationError(
                        g.Key,
                        g.Select(e => e.ErrorMessage)))
                    .ToList();
                
                errorResponse = ResponseDto<string>.Error(
                    "Validasyon hatası oluştu.",
                    validationErrors);
                statusCode = HttpStatusCode.BadRequest;
                break;

            case ArgumentException argumentException:
                errorResponse = ResponseDto<string>.Error(
                    argumentException.Message,
                    new ValidationError("Argument", argumentException.Message));
                statusCode = HttpStatusCode.BadRequest;
                break;

            case UnauthorizedAccessException:
                errorResponse = ResponseDto<string>.Error(
                    "Bu işlem için yetkiniz bulunmamaktadır.");
                statusCode = HttpStatusCode.Unauthorized;
                break;

            case KeyNotFoundException:
                errorResponse = ResponseDto<string>.Error(
                    "İstenen kayıt bulunamadı.");
                statusCode = HttpStatusCode.NotFound;
                break;

            default:
                var message = _env.IsDevelopment()
                    ? $"Bir hata oluştu: {exception.Message}"
                    : "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";

                if (_env.IsDevelopment())
                {
                    var debugError = new ValidationError(
                        "Debug",
                        new[]
                        {
                            $"Exception Type: {exception.GetType().Name}",
                            $"Stack Trace: {exception.StackTrace}"
                        });
                    errorResponse = ResponseDto<string>.Error(message, new List<ValidationError> { debugError });
                }
                else
                {
                    errorResponse = ResponseDto<string>.Error(message);
                }
                break;
        }

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = (int)statusCode
        };

        // Development ortamında detaylı loglama
        if (_env.IsDevelopment())
        {
            _logger.LogError(
                "Detailed Error: Type: {ExceptionType}, Message: {Message}, Stack: {StackTrace}",
                exception.GetType().Name,
                exception.Message,
                exception.StackTrace);
        }

        context.ExceptionHandled = true;
    }
}
