namespace InnoClinic.Profiles.Application.Dto;

/// <summary>
/// Data transfer object for filtering doctors
/// </summary>
public class DoctorFilterDto
{
    /// <summary>
    /// First name to filter by
    /// </summary>
    /// <example>John</example>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name to filter by
    /// </summary>
    /// <example>Doe</example>
    public string? LastName { get; set; }

    /// <summary>
    /// Specialization ID to filter by
    /// </summary>
    /// <example>87654321-4321-4321-4321-210987654321</example>
    public Guid? SpecializationId { get; set; }

    /// <summary>
    /// Office ID to filter by
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    public Guid? OfficeId { get; set; }

    /// <summary>
    /// Status to filter by
    /// </summary>
    /// <example>AtWork</example>
    public string? Status { get; set; }
}