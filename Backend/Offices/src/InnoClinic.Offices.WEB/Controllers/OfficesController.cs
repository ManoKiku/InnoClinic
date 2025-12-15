using InnoClinic.Offices.Application.Dto;
using InnoClinic.Offices.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Offices.WEB.Controllers;

/// <summary>
/// API controller for office management operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OfficesController : ControllerBase
{
    private readonly IOfficeService _officeService;
    private readonly ILogger<OfficesController> _logger;

    public OfficesController(IOfficeService officeService, ILogger<OfficesController> logger)
    {
        _officeService = officeService;
        _logger = logger;
    }

    /// <summary>
    /// Get all offices with optional filtering by activity status
    /// </summary>
    /// <param name="isActive">Filter by office activity status (true for active, false for inactive, null for all)</param>
    /// <returns>List of office DTOs</returns>
    /// <response code="200">Returns list of offices</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OfficeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OfficeDto>>> GetOffices([FromQuery] bool? isActive = null)
    {
        _logger.LogInformation("GET request for offices with filter: isActive={IsActive}", isActive);
        
        var offices = await _officeService.GetAllOfficesAsync(isActive);
        return Ok(offices);
    }

    /// <summary>
    /// Get office by identifier
    /// </summary>
    /// <param name="id">Office unique identifier (GUID)</param>
    /// <returns>Office DTO</returns>
    /// <response code="200">Returns the requested office</response>
    /// <response code="400">Invalid identifier format</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Office not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OfficeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OfficeDto>> GetOffice(Guid id)
    {
        _logger.LogInformation("GET request for office with ID: {Id}", id);
        
        var office = await _officeService.GetOfficeByIdAsync(id);
        return Ok(office);
    }

    /// <summary>
    /// Create a new office
    /// </summary>
    /// <param name="createOfficeDto">Office creation data</param>
    /// <returns>Created office DTO</returns>
    /// <response code="201">Office created successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="409">Conflict (office with same data already exists)</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(typeof(OfficeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OfficeDto>> CreateOffice([FromBody] CreateOfficeDto createOfficeDto)
    {
        _logger.LogInformation("POST request to create office at address: {Address}", createOfficeDto.Address);
        
        var office = await _officeService.CreateOfficeAsync(createOfficeDto);
        return CreatedAtAction(nameof(GetOffice), new { id = office.Id }, office);
    }

    /// <summary>
    /// Update existing office
    /// </summary>
    /// <param name="id">Office unique identifier</param>
    /// <param name="updateOfficeDto">Office update data</param>
    /// <returns>Updated office DTO</returns>
    /// <response code="200">Office updated successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Office not found</response>
    /// <response code="409">Conflict during update</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(OfficeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OfficeDto>> UpdateOffice(Guid id, [FromBody] UpdateOfficeDto updateOfficeDto)
    {
        _logger.LogInformation("PUT request to update office with ID: {Id}", id);
        
        var office = await _officeService.UpdateOfficeAsync(id, updateOfficeDto);
        return Ok(office);
    }

    /// <summary>
    /// Delete office
    /// </summary>
    /// <param name="id">Office unique identifier</param>
    /// <returns>No content</returns>
    /// <response code="204">Office deleted successfully</response>
    /// <response code="400">Invalid identifier</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Office not found</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOffice(Guid id)
    {
        _logger.LogInformation("DELETE request for office with ID: {Id}", id);
        
        await _officeService.DeleteOfficeAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Activate office
    /// </summary>
    /// <param name="id">Office unique identifier</param>
    /// <returns>Activated office DTO</returns>
    /// <response code="200">Office activated successfully</response>
    /// <response code="400">Invalid identifier</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Office not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPatch("{id}/activate")]
    [ProducesResponseType(typeof(OfficeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OfficeDto>> ActivateOffice(Guid id)
    {
        _logger.LogInformation("PATCH request to activate office with ID: {Id}", id);
        
        var office = await _officeService.ActivateOfficeAsync(id);
        return Ok(office);
    }

    /// <summary>
    /// Deactivate office
    /// </summary>
    /// <param name="id">Office unique identifier</param>
    /// <returns>Deactivated office DTO</returns>
    /// <response code="200">Office deactivated successfully</response>
    /// <response code="400">Invalid identifier</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Office not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPatch("{id}/deactivate")]
    [ProducesResponseType(typeof(OfficeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OfficeDto>> DeactivateOffice(Guid id)
    {
        _logger.LogInformation("PATCH request to deactivate office with ID: {Id}", id);
        
        var office = await _officeService.DeactivateOfficeAsync(id);
        return Ok(office);
    }
}