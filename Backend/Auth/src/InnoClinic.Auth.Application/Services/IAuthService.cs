using InnoClinic.Auth.Application.Dto;
using InnoClinic.Auth.Domain.Entities;

namespace InnoClinic.Auth.Application.Services;

public interface IAuthService
{
    Task<AuthResponse> SignUpAsync(SignUpRequest request, Guid? createdBy = null);
    Task<AuthResponse> SignInAsync(SignInRequest request);
    Task<bool> ValidateTokenAsync(string token);
    Task<Account?> GetUserByIdAsync(Guid userId);
    Task<Account?> UpdateUserAsync(Guid userId, UpdateAccountDto request, Guid updatedBy);
}