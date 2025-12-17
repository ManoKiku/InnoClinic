using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Profiles.Application.Dto.Receptionist;

/// <summary>
/// Data transfer object for updating an existing receptionist
/// </summary>
public class UpdateReceptionistDto
{
    /// <summary>
    /// Receptionist's first name
    /// </summary>
    /// <example>John</example>
    [MaxLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    /// Receptionist's last name
    /// </summary>
    /// <example>Doe</example>
    [MaxLength(50)]
    public string LastName { get; set; }

    /// <summary>
    /// Receptionist's middle name (optional)
    /// </summary>
    /// <example>Michael</example>
    [MaxLength(50)]
    public string MiddleName { get; set; }
    
    /// <summary>
    /// Office ID
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    public Guid? OfficeId { get; set; }
}