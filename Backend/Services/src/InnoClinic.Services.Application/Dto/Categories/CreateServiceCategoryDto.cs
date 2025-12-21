using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Services.Application.Dto.Categories;

/// <summary>
/// Data transfer object for creating a new service category
/// </summary>
public class CreateServiceCategoryDto
{
    /// <summary>
    /// Name of the category
    /// </summary>
    /// <example>Consultation</example>
    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; }

    /// <summary>
    /// Time slot size in minutes
    /// </summary>
    /// <example>30</example>
    [Required]
    [Range(5, 480)]
    public int TimeSlotSize { get; set; }
}