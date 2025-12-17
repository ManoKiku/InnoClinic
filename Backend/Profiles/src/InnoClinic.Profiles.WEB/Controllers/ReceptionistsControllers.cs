using InnoClinic.Profiles.Application.Dto.Receptionist;
using InnoClinic.Profiles.Application.Dto;
using InnoClinic.Profiles.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.WEB.Controllers;

/// <summary>
/// Controller for managing Receptionist profiles
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ReceptionistsController : ControllerBase
{
    private readonly IReceptionistService _receptionistService;

    public ReceptionistsController(IReceptionistService receptionistService)
    {
        _receptionistService = receptionistService;
    }

    /// <summary>
    /// Get receptionist by ID
    /// </summary>
    /// <param name="id">Receptionist's unique identifier</param>
    /// <returns>Receptionist details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReceptionistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReceptionistDto>> GetById(Guid id)
    {
        var receptionist = await _receptionistService.GetByIdAsync(id);
        return Ok(receptionist);
    }

    /// <summary>
    /// Get receptionist by account ID
    /// </summary>
    /// <param name="accountId">Associated account ID</param>
    /// <returns>Receptionist details</returns>
    [HttpGet("account/{accountId}")]
    [ProducesResponseType(typeof(ReceptionistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReceptionistDto>> GetByAccountId(Guid accountId)
    {
        var receptionist = await _receptionistService.GetByAccountIdAsync(accountId);
        return Ok(receptionist);
    }

    /// <summary>
    /// Get all receptionists with filtering and pagination
    /// </summary>
    /// <param name="filter">Filtering and pagination parameters</param>
    /// <returns>List of Receptionists</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReceptionistDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ReceptionistDto>>> GetAll([FromQuery] ReceptionistFilterDto filter)
    {
        var receptionist = await _receptionistService.GetAllAsync(filter);
        return Ok(receptionist);
    }

    /// <summary>
    /// Create a new receptionist
    /// </summary>
    /// <param name="receptionistDto">Receptionist creation data</param>
    /// <returns>Created receptionist details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ReceptionistDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReceptionistDto>> Create([FromBody] CreateReceptionistDto receptionistDto)
    {
        var createdReceptionist = await _receptionistService.CreateAsync(receptionistDto);
        return CreatedAtAction(nameof(GetById), new { id = createdReceptionist.Id }, createdReceptionist);
    }

    /// <summary>
    /// Update an existing receptionist
    /// </summary>
    /// <param name="id">Receptionist's unique identifier</param>
    /// <param name="receptionistDto">Receptionist update data</param>
    /// <returns>Updated Receptionist details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ReceptionistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReceptionistDto>> Update(Guid id, [FromBody] UpdateReceptionistDto receptionistDto)
    {
        var updatedReceptionist = await _receptionistService.UpdateAsync(id, receptionistDto);
        return Ok(updatedReceptionist);
    }

    /// <summary>
    /// Delete a receptionist by ID
    /// </summary>
    /// <param name="id">Receptionist's unique identifier</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _receptionistService.DeleteAsync(id);
        return NoContent();
    }
}