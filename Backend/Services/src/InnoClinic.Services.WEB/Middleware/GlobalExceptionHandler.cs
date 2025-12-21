using System.Net;
using InnoClinic.Services.Application.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InnoClinic.Services.WEB.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly bool _isDevelopment;
    
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _isDevelopment = environment.IsDevelopment();
    }
    
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        
        var errorResponse = new ErrorResponse
        {
            Timestamp = DateTime.UtcNow
        };
        
        switch (exception)
        {
            case ApplicationException appEx:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = appEx.Message;
                break;
                
            case SecurityTokenException tokenEx:
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "Invalid or expired token";
                if (_isDevelopment) errorResponse.Details = tokenEx.Message;
                break;
                
            case DbUpdateException dbEx:
                httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                errorResponse.Message = "Database update error";
                if (_isDevelopment) errorResponse.Details = dbEx.InnerException?.Message ?? dbEx.Message;
                break;
                
            case UnauthorizedAccessException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "Access denied";
                break;
                
            case KeyNotFoundException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = "Resource not found";
                break;
                
            case ArgumentException argEx:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid input";
                if (_isDevelopment) errorResponse.Details = argEx.Message;
                break;
                
            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "An internal server error occurred";
                if (_isDevelopment) errorResponse.Details = exception.Message;
                break;
        }
        
        httpContext.Response.ContentType = "application/json";
        
        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        
        return true;
    }
}