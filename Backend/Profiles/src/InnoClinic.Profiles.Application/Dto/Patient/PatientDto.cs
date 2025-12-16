namespace InnoClinic.Profiles.Application.Dto.Patient;


/// <summary>
/// Data transfer object for Patient response
/// </summary>
public class PatientDto
{
    /// <summary>
    /// Patient's unique identifier
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Patient's first name
    /// </summary>
    /// <example>John</example>
    public string FirstName { get; set; }

    /// <summary>
    /// Patient's last name
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; }

    /// <summary>
    /// Patient's middle name
    /// </summary>
    /// <example>Michael</example>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Patient's date of birth
    /// </summary>
    /// <example>1980-05-15</example>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Associated account ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid AccountId { get; set; }
    
    /// <summary>
    /// Is patient linked to account
    /// </summary>
    /// <example>true</example>
    public bool IsLinkedToAccount { get; set; }
}