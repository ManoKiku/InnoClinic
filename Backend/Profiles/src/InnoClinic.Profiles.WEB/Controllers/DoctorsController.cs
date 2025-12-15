using InnoClinic.Profiles.Application.Dto;
using InnoClinic.Profiles.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.WEB.Controllers;

/// <summary>
/// Controller for managing doctor profiles
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    /// <summary>
    /// Get doctor by ID
    /// </summary>
    /// <param name="id">Doctor's unique identifier</param>
    /// <returns>Doctor details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorDto>> GetById(Guid id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        return Ok(doctor);
    }

    /// <summary>
    /// Get doctor by account ID
    /// </summary>
    /// <param name="accountId">Associated account ID</param>
    /// <returns>Doctor details</returns>
    [HttpGet("account/{accountId}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorDto>> GetByAccountId(Guid accountId)
    {
        var doctor = await _doctorService.GetByAccountIdAsync(accountId);
        return Ok(doctor);
    }

    /// <summary>
    /// Get all doctors with filtering and pagination
    /// </summary>
    /// <param name="filter">Filtering and pagination parameters</param>
    /// <returns>List of doctors</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DoctorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAll([FromQuery] DoctorFilterDto filter)
    {
        var doctors = await _doctorService.GetAllAsync(filter);
        return Ok(doctors);
    }

    /// <summary>
    /// Create a new doctor
    /// </summary>
    /// <param name="doctorDto">Doctor creation data</param>
    /// <returns>Created doctor details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorDto>> Create([FromBody] CreateDoctorDto doctorDto)
    {
        var createdDoctor = await _doctorService.CreateAsync(doctorDto);
        return CreatedAtAction(nameof(GetById), new { id = createdDoctor.Id }, createdDoctor);
    }

    /// <summary>
    /// Update an existing doctor
    /// </summary>
    /// <param name="id">Doctor's unique identifier</param>
    /// <param name="doctorDto">Doctor update data</param>
    /// <returns>Updated doctor details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorDto>> Update(Guid id, [FromBody] UpdateDoctorDto doctorDto)
    {
        var updatedDoctor = await _doctorService.UpdateAsync(id, doctorDto);
        return Ok(updatedDoctor);
    }

    /// <summary>
    /// Delete a doctor by ID
    /// </summary>
    /// <param name="id">Doctor's unique identifier</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _doctorService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Change doctor status
    /// </summary>
    /// <param name="id">Doctor's unique identifier</param>
    /// <param name="status">New status value</param>
    /// <returns>Updated doctor details</returns>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(typeof(DoctorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorDto>> ChangeStatus(Guid id, [FromBody] string status)
    {
        var updatedDoctor = await _doctorService.ChangeStatusAsync(id, status);
        return Ok(updatedDoctor);
    }
}