using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Services.Application.Dto.Categories;

/// <summary>
/// Data transfer object for updating an existing service category
/// </summary>
public class UpdateServiceCategoryDto
{
    /// <summary>
    /// Name of the category
    /// </summary>
    /// <example>Consultation</example>
    [MaxLength(100)]
    public string CategoryName { get; set; }

    /// <summary>
    /// Time slot size in minutes
    /// </summary>
    /// <example>30</example>
    [Range(5, 480)]
    public int? TimeSlotSize { get; set; }
}