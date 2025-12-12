using System.Security.Claims;
using InnoClinic.Auth.Application.Dto;
using InnoClinic.Auth.Domain.Entities;

namespace InnoClinic.Auth.Application.Services;

public interface IJwtTokenService
{
    string GenerateAccessToken(Account account);
    ClaimsPrincipal? ValidateToken(string token);
    AuthResponse GenerateAuthResponse(Account account);
}