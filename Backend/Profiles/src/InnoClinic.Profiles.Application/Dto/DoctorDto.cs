namespace InnoClinic.Profiles.Application.Dto;

/// <summary>
/// Data transfer object for doctor response
/// </summary>
public class DoctorDto
{
    /// <summary>
    /// Doctor's unique identifier
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Doctor's first name
    /// </summary>
    /// <example>John</example>
    public string FirstName { get; set; }

    /// <summary>
    /// Doctor's last name
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; }

    /// <summary>
    /// Doctor's middle name
    /// </summary>
    /// <example>Michael</example>
    public string MiddleName { get; set; }

    /// <summary>
    /// Doctor's date of birth
    /// </summary>
    /// <example>1980-05-15</example>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Associated account ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Specialization ID
    /// </summary>
    /// <example>87654321-4321-4321-4321-210987654321</example>
    public Guid SpecializationId { get; set; }

    /// <summary>
    /// Office ID
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    public Guid OfficeId { get; set; }

    /// <summary>
    /// Year when the doctor started their career
    /// </summary>
    /// <example>2005</example>
    public int CareerStartYear { get; set; }

    /// <summary>
    /// Doctor's current status
    /// </summary>
    /// <example>AtWork</example>
    public string Status { get; set; }

    /// <summary>
    /// Doctor's experience in years
    /// </summary>
    /// <example>18</example>
    public int Experience { get; set; }
}