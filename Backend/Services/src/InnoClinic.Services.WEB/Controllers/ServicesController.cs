using InnoClinic.Services.Application.Dto;
using InnoClinic.Services.Application.Dto.Services;
using InnoClinic.Services.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Services.WEB.Controllers;

/// <summary>
/// Controller for managing medical services
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ServicesController : ControllerBase
{
    private readonly IServicesService _servicesService;

    public ServicesController(IServicesService servicesService)
    {
        _servicesService = servicesService;
    }

    /// <summary>
    /// Get service by ID
    /// </summary>
    /// <param name="id">Service unique identifier</param>
    /// <returns>Service details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceDto>> GetById(Guid id)
    {
        var service = await _servicesService.GetByIdAsync(id);
        return Ok(service);
    }

    /// <summary>
    /// Get all services with filtering and pagination
    /// </summary>
    /// <param name="filter">Filtering and pagination parameters</param>
    /// <returns>List of services</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll([FromQuery] ServiceFilterDto filter)
    {
        var services = await _servicesService.GetAllAsync(filter);
        return Ok(services);
    }

    /// <summary>
    /// Get services by specialization ID
    /// </summary>
    /// <param name="specializationId">Specialization unique identifier</param>
    /// <returns>List of services</returns>
    [HttpGet("specialization/{specializationId}")]
    [ProducesResponseType(typeof(IEnumerable<ServiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetBySpecializationId(Guid specializationId)
    {
        var services = await _servicesService.GetBySpecializationIdAsync(specializationId);
        return Ok(services);
    }

    /// <summary>
    /// Get services by category ID
    /// </summary>
    /// <param name="categoryId">Category unique identifier</param>
    /// <returns>List of services</returns>
    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(typeof(IEnumerable<ServiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetByCategoryId(Guid categoryId)
    {
        var services = await _servicesService.GetByCategoryIdAsync(categoryId);
        return Ok(services);
    }

    /// <summary>
    /// Create a new service
    /// </summary>
    /// <param name="serviceDto">Service creation data</param>
    /// <returns>Created service details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceDto>> Create([FromBody] CreateServiceDto serviceDto)
    {
        var createdService = await _servicesService.CreateAsync(serviceDto);
        return CreatedAtAction(nameof(GetById), new { id = createdService.Id }, createdService);
    }

    /// <summary>
    /// Update an existing service
    /// </summary>
    /// <param name="id">Service unique identifier</param>
    /// <param name="serviceDto">Service update data</param>
    /// <returns>Updated service details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceDto>> Update(Guid id, [FromBody] UpdateServiceDto serviceDto)
    {
        var updatedService = await _servicesService.UpdateAsync(id, serviceDto);
        return Ok(updatedService);
    }

    /// <summary>
    /// Change service status
    /// </summary>
    /// <param name="id">Service unique identifier</param>
    /// <param name="status">New status value</param>
    /// <returns>Updated service details</returns>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceDto>> ChangeStatus(Guid id, [FromBody] bool status)
    {
        var updatedService = await _servicesService.ChangeStatusAsync(id, status);
        return Ok(updatedService);
    }
}