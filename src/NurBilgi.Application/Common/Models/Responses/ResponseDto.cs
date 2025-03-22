using NurBilgi.Application.Common.Models.Errors;
using NurBilgi.Application.Features.Auth.Commands.Login;

namespace NurBilgi.Application.Common.Models.Responses;

public sealed record ResponseDto<T>
{
    public string? Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public IReadOnlyList<ValidationError> Errors { get; set; } = [];

    public ResponseDto()
    {

    }

    public ResponseDto(T data)
    {
        Data = data;
    }

    public ResponseDto(string message, bool success)
    {
        Message = message;
        IsSuccess = success;
    }

    public ResponseDto(T data, string message) : this(data)
    {
        Message = message;
    }


    public ResponseDto(string message, List<ValidationError> errors)
    {
        Message = message;
        Errors = errors;
        IsSuccess = false;
    }

    public ResponseDto(T data, string message, List<ValidationError> errors) : this(message, errors)
    {
        Data = data;
        IsSuccess = false;
    }

    public ResponseDto(T data, string message, bool success, List<ValidationError> errors) : this(data, message, errors)
    {
        IsSuccess = success;

    }
    public ResponseDto(T? data, string? message, bool isSuccess, IReadOnlyList<ValidationError>? validationErrors = null)
    {
        Data = data;
        Message = message;
        IsSuccess = isSuccess;
        Errors = validationErrors ?? Array.Empty<ValidationError>();
    }
    public static ResponseDto<T> Success(T data, string message)
        => new(data, message, true);

    public static ResponseDto<T?> Success(string message)
        => new(default, message, true);

    public static ResponseDto<T> Error(string message, List<ValidationError>? validationErrors = null)
        => new(default, message, false, validationErrors);

    public static ResponseDto<T> Error(string message, ValidationError validationError)
        => new(default, message, false, new[] { validationError });
}