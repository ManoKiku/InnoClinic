namespace InnoClinic.Profiles.Application.Dto.Receptionist;

/// <summary>
/// Data transfer object for filtering doctors
/// </summary>
public class ReceptionistFilterDto
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
    /// Office ID to filter by
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    public Guid? OfficeId { get; set; }
}