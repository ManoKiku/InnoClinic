using System.Security.Cryptography;
using InnoClinic.Auth.Application.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace InnoClinic.Auth.Application.Services;

public class PasswordHashService : IPasswordHashService
{
    private readonly PasswordHashSettings _settings;
    
    public PasswordHashService(IOptions<PasswordHashSettings> settings)
    {
        _settings = settings.Value;
    }
    
    public string HashPassword(string password)
    {
        byte[] salt = new byte[_settings.SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        
        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: _settings.Iterations,
            numBytesRequested: _settings.HashSize
        );
        
        byte[] hashBytes = new byte[_settings.SaltSize + _settings.HashSize];
        Array.Copy(salt, 0, hashBytes, 0, _settings.SaltSize);
        Array.Copy(hash, 0, hashBytes, _settings.SaltSize, _settings.HashSize);
        
        return Convert.ToBase64String(hashBytes);
    }
    
    public bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        
        byte[] salt = new byte[_settings.SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, _settings.SaltSize);
        
        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: _settings.Iterations,
            numBytesRequested: _settings.HashSize
        );
        
        for (int i = 0; i < _settings.HashSize; i++)
        {
            if (hashBytes[i + _settings.SaltSize] != hash[i])
            {
                return false;
            }
        }
        
        return true;
    }
}