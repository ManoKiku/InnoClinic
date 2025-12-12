using System.Security.Claims;
using InnoClinic.Auth.Application.Data;
using InnoClinic.Auth.Application.Dto;
using InnoClinic.Auth.Application.Models;
using InnoClinic.Auth.Application.Services;
using InnoClinic.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InnoClinic.Auth.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly AuthSettings _authSettings;
    
    public AuthService(
        ApplicationDbContext context,
        IPasswordHashService passwordHashService,
        IJwtTokenService jwtTokenService,
        IOptions<AuthSettings> authSettings)
    {
        _context = context;
        _passwordHashService = passwordHashService;
        _jwtTokenService = jwtTokenService;
        _authSettings = authSettings.Value;
    }
    
    public async Task<AuthResponse> SignUpAsync(SignUpRequest request, Guid? createdBy = null)
    {
        var existingAccount = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Email == request.Email);
        
        if (existingAccount != null)
        {
            throw new ApplicationException($"User with email '{request.Email}' already exists.");
        }
        
        if (request.Password != request.PasswordConfirm)
        {
            throw new ApplicationException("Passwords do not match.");
        }
        
        var account = new Account
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = _passwordHashService.HashPassword(request.Password),
            PhoneNumber = string.Empty,
            IsEmailVerified = !_authSettings.RequireEmailVerification,
            PhotoId = Guid.Empty,
            CreatedBy = createdBy ?? Guid.Empty,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = createdBy ?? Guid.Empty,
            UpdatedAt = DateTime.UtcNow
        };
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        
        return _jwtTokenService.GenerateAuthResponse(account);
    }
    
    public async Task<AuthResponse> SignInAsync(SignInRequest request)
    {
        var account = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Email == request.Email);
        
        if (account == null)
        {
            throw new ApplicationException("Invalid email or password.");
        }
        
        if (!_passwordHashService.VerifyPassword(request.Password, account.PasswordHash))
        {
            throw new ApplicationException("Invalid email or password.");
        }
        
        if (_authSettings.RequireEmailVerification && !account.IsEmailVerified)
        {
            throw new ApplicationException("Email not verified. Please check your email.");
        }
        
        account.UpdatedAt = DateTime.UtcNow;
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        
        return _jwtTokenService.GenerateAuthResponse(account);
    }
    
    public async Task<bool> ValidateTokenAsync(string token)
    {
        var principal = _jwtTokenService.ValidateToken(token);
        if (principal == null)
            return false;
        
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return false;
        
        var accountExists = await _context.Accounts
            .AsNoTracking()
            .AnyAsync(a => a.Id == userId);
        
        return accountExists;
    }
    
    public async Task<Account?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == userId);
    }

    public async Task<Account?> UpdateUserAsync(Guid userId, UpdateAccountDto request, Guid updatedBy)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == userId);
            
        if (account == null)
            return null;
            
        account.UpdatedBy = updatedBy;
        account.UpdatedAt = DateTime.UtcNow;
            
        await _context.SaveChangesAsync();

        return account;
    }
}