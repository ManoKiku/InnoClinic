namespace InnoClinic.Services.Application.Dto.Categories;

/// <summary>
/// Data transfer object for service category response
/// </summary>
public class ServiceCategoryDto
{
    /// <summary>
    /// Unique identifier of the category
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the category
    /// </summary>
    /// <example>Consultation</example>
    public string CategoryName { get; set; }

    /// <summary>
    /// Time slot size in minutes
    /// </summary>
    /// <example>30</example>
    public int TimeSlotSize { get; set; }

    /// <summary>
    /// Number of services in this category
    /// </summary>
    /// <example>15</example>
    public int ServiceCount { get; set; }
}