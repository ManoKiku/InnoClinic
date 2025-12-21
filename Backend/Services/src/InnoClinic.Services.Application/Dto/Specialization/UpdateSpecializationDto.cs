using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Services.Application.Dto.Specialization;

/// <summary>
/// Data transfer object for updating an existing specialization
/// </summary>
public class UpdateSpecializationDto
{
    /// <summary>
    /// Name of the specialization
    /// </summary>
    /// <example>Cardiology</example>
    [MaxLength(100)]
    public string SpecializationName { get; set; }

    /// <summary>
    /// Active status
    /// </summary>
    /// <example>true</example>
    public bool? IsActive { get; set; }
}