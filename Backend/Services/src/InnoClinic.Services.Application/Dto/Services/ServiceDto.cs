namespace InnoClinic.Services.Application.Dto.Services;

/// <summary>
/// Data transfer object for service response
/// </summary>
public class ServiceDto
{
    /// <summary>
    /// Unique identifier of the service
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the service
    /// </summary>
    /// <example>Initial Cardiology Consultation</example>
    public string ServiceName { get; set; }

    /// <summary>
    /// Price of the service
    /// </summary>
    /// <example>150.00</example>
    public int Price { get; set; }

    /// <summary>
    /// Category ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    /// <example>Consultation</example>
    public string CategoryName { get; set; }

    /// <summary>
    /// Specialization ID
    /// </summary>
    /// <example>87654321-4321-4321-4321-210987654321</example>
    public Guid SpecializationId { get; set; }

    /// <summary>
    /// Specialization name
    /// </summary>
    /// <example>Cardiology</example>
    public string SpecializationName { get; set; }

    /// <summary>
    /// Service status
    /// </summary>
    /// <example>Active</example>
    public string Status { get; set; }
    
    /// <summary>
    /// Time slot size from category in minutes
    /// </summary>
    /// <example>30</example>
    public int TimeSlotSize { get; set; }
}