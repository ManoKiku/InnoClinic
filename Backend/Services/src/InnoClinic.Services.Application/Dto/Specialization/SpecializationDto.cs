namespace InnoClinic.Services.Application.Dto.Specialization;

/// <summary>
/// Data transfer object for specialization response
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// Unique identifier of the specialization
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the specialization
    /// </summary>
    /// <example>Cardiology</example>
    public string SpecializationName { get; set; }

    /// <summary>
    /// Indicates if the specialization is active
    /// </summary>
    /// <example>true</example>
    public bool IsActive { get; set; }

    /// <summary>
    /// Number of services in this specialization
    /// </summary>
    /// <example>5</example>
    public int ServiceCount { get; set; }
}