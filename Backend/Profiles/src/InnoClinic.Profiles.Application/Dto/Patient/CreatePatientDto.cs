using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Profiles.Application.Dto.Patient;


/// <summary>
/// Data transfer object for creating a new Patient
/// </summary>
public class CreatePatientDto
{
    /// <summary>
    /// Patient's first name
    /// </summary>
    /// <example>John</example>
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    /// Patient's last name
    /// </summary>
    /// <example>Doe</example>
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    /// <summary>
    /// Patient's middle name (optional)
    /// </summary>
    /// <example>Michael</example>
    [MaxLength(50)]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Patient's date of birth
    /// </summary>
    /// <example>1980-05-15</example>
    [Required]
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Associated account ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    [Required]
    public Guid AccountId { get; set; }
}