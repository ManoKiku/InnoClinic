using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Services.Application.Dto.Specialization;

/// <summary>
/// Data transfer object for creating a new specialization
/// </summary>
public class CreateSpecializationDto
{
    /// <summary>
    /// Name of the specialization
    /// </summary>
    /// <example>Cardiology</example>
    [Required]
    [MaxLength(100)]
    public string SpecializationName { get; set; }

    /// <summary>
    /// Initial active status
    /// </summary>
    /// <example>true</example>
    public bool IsActive { get; set; } = true;
}