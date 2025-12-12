using System.Security.Claims;
using InnoClinic.Auth.Application.Dto;
using InnoClinic.Auth.Application.Services;
using InnoClinic.Auth.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Auth.WEB.Controllers;

/// <summary>
/// Authentication controller for user registration, login, and account management
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    
    /// <summary>
    /// Register a new user account
    /// </summary>
    /// <param name="request">User registration data</param>
    /// <returns>Authentication token and user information</returns>
    [HttpPost("signup")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponse>> SignUp([FromBody] SignUpRequest request)
    {
        _logger.LogInformation("Sign up attempt for email: {Email}", request.Email);
        
        Guid? createdBy = null;
        // If admin creates user, get admin ID from token
        if (User.Identity?.IsAuthenticated == true)
        {
            var createdByClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(createdByClaim, out var createdByGuid))
            {
                createdBy = createdByGuid;
            }
        }
        
        var result = await _authService.SignUpAsync(request, createdBy);
        
        _logger.LogInformation("User {Email} registered successfully", request.Email);
        return CreatedAtAction(nameof(SignUp), result);
    }
    
    /// <summary>
    /// Authenticate existing user
    /// </summary>
    /// <param name="request">User credentials</param>
    /// <returns>Authentication token and user information</returns>
    [HttpPost("signin")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponse>> SignIn([FromBody] SignInRequest request)
    {
        _logger.LogInformation("Sign in attempt for email: {Email}", request.Email);
        
        var result = await _authService.SignInAsync(request);
        
        _logger.LogInformation("User {Email} signed in successfully", request.Email);
        return Ok(result);
    }
    
    /// <summary>
    /// Validate JWT token
    /// </summary>
    /// <param name="token">JWT token to validate</param>
    /// <returns>Validation result</returns>
    [HttpGet("validate")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> ValidateToken([FromQuery] string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return BadRequest("Token is required");
        }
        
        var isValid = await _authService.ValidateTokenAsync(token);
        return Ok(isValid);
    }
    
    /// <summary>
    /// Get current user profile
    /// </summary>
    /// <returns>User profile information</returns>
    [Authorize]
    [HttpGet("profile")]
    [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Account>> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
        {
            return Unauthorized(new ErrorResponse
            {
                Message = "Invalid token"
            });
        }
        
        var user = await _authService.GetUserByIdAsync(userGuid);
        
        if (user == null)
        {
            return NotFound(new ErrorResponse
            {
                Message = "User not found"
            });
        }
        
        return Ok(user);
    }
    
    /// <summary>
    /// Update current user profile
    /// </summary>
    /// <param name="request">Update data</param>
    /// <returns>Updated user profile</returns>
    [Authorize]
    [HttpPut("profile")]
    [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Account>> UpdateProfile([FromBody] UpdateAccountDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
        {
            return Unauthorized(new ErrorResponse
            {
                Message = "Invalid token"
            });
        }
        
        var updatedUser = await _authService.UpdateUserAsync(userGuid, request, userGuid);
        
        if (updatedUser == null)
        {
            return NotFound(new ErrorResponse
            {
                Message = "User not found"
            });
        }
        
        _logger.LogInformation("User {UserId} updated their profile", userGuid);
        return Ok(updatedUser);
    }
}