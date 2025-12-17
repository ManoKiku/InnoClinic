namespace InnoClinic.Profiles.Application.Dto.Receptionist;

/// <summary>
/// Data transfer object for receptionist response
/// </summary>
public class ReceptionistDto
{
    /// <summary>
    /// Receptionist's unique identifier
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Receptionist's first name
    /// </summary>
    /// <example>John</example>
    public string FirstName { get; set; }

    /// <summary>
    /// Receptionist's last name
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; }

    /// <summary>
    /// Receptionist's middle name
    /// </summary>
    /// <example>Michael</example>
    public string MiddleName { get; set; }

    /// <summary>
    /// Associated account ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Office ID
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    public Guid OfficeId { get; set; }
}