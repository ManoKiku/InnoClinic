using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Profiles.Application.Dto.Doctor;

/// <summary>
/// Data transfer object for updating an existing doctor
/// </summary>
public class UpdateDoctorDto
{
    /// <summary>
    /// Doctor's first name
    /// </summary>
    /// <example>John</example>
    [MaxLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    /// Doctor's last name
    /// </summary>
    /// <example>Doe</example>
    [MaxLength(50)]
    public string LastName { get; set; }

    /// <summary>
    /// Doctor's middle name (optional)
    /// </summary>
    /// <example>Michael</example>
    [MaxLength(50)]
    public string MiddleName { get; set; }

    /// <summary>
    /// Doctor's date of birth
    /// </summary>
    /// <example>1980-05-15</example>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Specialization ID
    /// </summary>
    /// <example>87654321-4321-4321-4321-210987654321</example>
    public Guid? SpecializationId { get; set; }

    /// <summary>
    /// Office ID
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    public Guid? OfficeId { get; set; }

    /// <summary>
    /// Year when the doctor started their career
    /// </summary>
    /// <example>2005</example>
    [Range(1900, 2100)]
    public int? CareerStartYear { get; set; }

    /// <summary>
    /// Doctor's status
    /// </summary>
    /// <example>AtWork</example>
    public string Status { get; set; }
}