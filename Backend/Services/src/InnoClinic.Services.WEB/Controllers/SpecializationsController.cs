using InnoClinic.Services.Application.Dto;
using InnoClinic.Services.Application.Dto.Specialization;
using InnoClinic.Services.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Services.WEB.Controllers;

/// <summary>
/// Controller for managing medical specializations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationsController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    /// <summary>
    /// Get specialization by ID
    /// </summary>
    /// <param name="id">Specialization unique identifier</param>
    /// <returns>Specialization details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> GetById(Guid id)
    {
        var specialization = await _specializationService.GetByIdAsync(id);
        return Ok(specialization);
    }

    /// <summary>
    /// Get all specializations with filtering and pagination
    /// </summary>
    /// <param name="filter">Filtering and pagination parameters</param>
    /// <returns>List of specializations</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAll([FromQuery] SpecializationFilterDto filter)
    {
        var specializations = await _specializationService.GetAllAsync(filter);
        return Ok(specializations);
    }

    /// <summary>
    /// Create a new specialization
    /// </summary>
    /// <param name="specializationDto">Specialization creation data</param>
    /// <returns>Created specialization details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Create([FromBody] CreateSpecializationDto specializationDto)
    {
        var createdSpecialization = await _specializationService.CreateAsync(specializationDto);
        return CreatedAtAction(nameof(GetById), new { id = createdSpecialization.Id }, createdSpecialization);
    }

    /// <summary>
    /// Update an existing specialization
    /// </summary>
    /// <param name="id">Specialization unique identifier</param>
    /// <param name="specializationDto">Specialization update data</param>
    /// <returns>Updated specialization details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Update(Guid id, [FromBody] UpdateSpecializationDto specializationDto)
    {
        var updatedSpecialization = await _specializationService.UpdateAsync(id, specializationDto);
        return Ok(updatedSpecialization);
    }

    /// <summary>
    /// Change specialization status
    /// </summary>
    /// <param name="id">Specialization unique identifier</param>
    /// <param name="isActive">New active status</param>
    /// <returns>Updated specialization details</returns>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> ChangeStatus(Guid id, [FromBody] bool isActive)
    {
        var updatedSpecialization = await _specializationService.ChangeStatusAsync(id, isActive);
        return Ok(updatedSpecialization);
    }
}