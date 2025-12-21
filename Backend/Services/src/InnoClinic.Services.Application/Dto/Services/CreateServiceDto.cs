using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Services.Application.Dto.Services;

/// <summary>
/// Data transfer object for creating a new service
/// </summary>
public class CreateServiceDto
{
    /// <summary>
    /// Name of the service
    /// </summary>
    /// <example>Initial Cardiology Consultation</example>
    [Required]
    [MaxLength(200)]
    public string ServiceName { get; set; }

    /// <summary>
    /// Price of the service
    /// </summary>
    /// <example>150.00</example>
    [Required]
    [Range(1, 1000000)]
    public int Price { get; set; }

    /// <summary>
    /// Category ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    [Required]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Specialization ID
    /// </summary>
    /// <example>87654321-4321-4321-4321-210987654321</example>
    [Required]
    public Guid SpecializationId { get; set; }

    /// <summary>
    /// Service status
    /// </summary>
    /// <example>Active</example>
    public bool Status { get; set; } = true;
}