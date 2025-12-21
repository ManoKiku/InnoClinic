using InnoClinic.Services.Application.Dto.Categories;
using InnoClinic.Services.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Services.WEB.Controllers;

/// <summary>
/// Controller for managing service categories
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ServiceCategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public ServiceCategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get category by ID
    /// </summary>
    /// <param name="id">Category unique identifier</param>
    /// <returns>Category details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ServiceCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceCategoryDto>> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return Ok(category);
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns>List of categories</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceCategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ServiceCategoryDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Create a new category
    /// </summary>
    /// <param name="categoryDto">Category creation data</param>
    /// <returns>Created category details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ServiceCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceCategoryDto>> Create([FromBody] CreateServiceCategoryDto categoryDto)
    {
        var createdCategory = await _categoryService.CreateAsync(categoryDto);
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    /// <summary>
    /// Update an existing category
    /// </summary>
    /// <param name="id">Category unique identifier</param>
    /// <param name="categoryDto">Category update data</param>
    /// <returns>Updated category details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ServiceCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceCategoryDto>> Update(Guid id, [FromBody] UpdateServiceCategoryDto categoryDto)
    {
        var updatedCategory = await _categoryService.UpdateAsync(id, categoryDto);
        return Ok(updatedCategory);
    }

    /// <summary>
    /// Delete a category by ID
    /// </summary>
    /// <param name="id">Category unique identifier</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}