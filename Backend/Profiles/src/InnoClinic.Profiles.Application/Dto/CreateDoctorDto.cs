using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Profiles.Application.Dto;

/// <summary>
/// Data transfer object for creating a new doctor
/// </summary>
public class CreateDoctorDto
{
    /// <summary>
    /// Doctor's first name
    /// </summary>
    /// <example>John</example>
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    /// Doctor's last name
    /// </summary>
    /// <example>Doe</example>
    [Required]
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
    [Required]
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Associated account ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// Specialization ID
    /// </summary>
    /// <example>87654321-4321-4321-4321-210987654321</example>
    [Required]
    public Guid SpecializationId { get; set; }

    /// <summary>
    /// Office ID
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    [Required]
    public Guid OfficeId { get; set; }

    /// <summary>
    /// Year when the doctor started their career
    /// </summary>
    /// <example>2005</example>
    [Required]
    [Range(1900, 2100)]
    public int CareerStartYear { get; set; }
}