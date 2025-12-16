using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Profiles.Application.Dto.Patient;

/// <summary>
/// Data transfer object for updating an existing Patient
/// </summary>
public class UpdatePatientDto
{
    /// <summary>
    /// Patient's first name
    /// </summary>
    /// <example>John</example>
    [MaxLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    /// Patient's last name
    /// </summary>
    /// <example>Doe</example>
    [MaxLength(50)]
    public string LastName { get; set; }

    /// <summary>
    /// Patient's middle name (optional)
    /// </summary>
    /// <example>Michael</example>
    [MaxLength(50)]
    public string MiddleName { get; set; }

    /// <summary>
    /// Patient's date of birth
    /// </summary>
    /// <example>1980-05-15</example>
    public DateTime? DateOfBirth { get; set; }
}