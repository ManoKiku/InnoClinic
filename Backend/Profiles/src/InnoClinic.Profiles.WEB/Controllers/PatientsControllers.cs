using InnoClinic.Profiles.Application.Dto.Patient;
using InnoClinic.Profiles.Application.Dto;
using InnoClinic.Profiles.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.WEB.Controllers;

/// <summary>
/// Controller for managing Patient profiles
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Get patient by ID
    /// </summary>
    /// <param name="id">Patient's unique identifier</param>
    /// <returns>Patient details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientDto>> GetById(Guid id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        return Ok(patient);
    }

    /// <summary>
    /// Get patient by account ID
    /// </summary>
    /// <param name="accountId">Associated account ID</param>
    /// <returns>Patient details</returns>
    [HttpGet("account/{accountId}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientDto>> GetByAccountId(Guid accountId)
    {
        var patient = await _patientService.GetByAccountIdAsync(accountId);
        return Ok(patient);
    }

    /// <summary>
    /// Get all Patients with filtering and pagination
    /// </summary>
    /// <param name="filter">Filtering and pagination parameters</param>
    /// <returns>List of Patients</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll([FromQuery] PatientFilterDto filter)
    {
        var patient = await _patientService.GetAllAsync(filter);
        return Ok(patient);
    }

    /// <summary>
    /// Create a new patient
    /// </summary>
    /// <param name="patientDto">Patient creation data</param>
    /// <returns>Created patient details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientDto>> Create([FromBody] CreatePatientDto patientDto)
    {
        var createdPatient = await _patientService.CreateAsync(patientDto);
        return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, createdPatient);
    }

    /// <summary>
    /// Update an existing Patient
    /// </summary>
    /// <param name="id">Patient's unique identifier</param>
    /// <param name="patientDto">Patient update data</param>
    /// <returns>Updated patient details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientDto>> Update(Guid id, [FromBody] UpdatePatientDto patientDto)
    {
        var updatedPatient = await _patientService.UpdateAsync(id, patientDto);
        return Ok(updatedPatient);
    }

    /// <summary>
    /// Delete a patient by ID
    /// </summary>
    /// <param name="id">Patient's unique identifier</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeleteAsync(id);
        return NoContent();
    }
}