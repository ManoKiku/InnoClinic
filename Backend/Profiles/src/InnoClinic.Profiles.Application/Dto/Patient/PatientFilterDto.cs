namespace InnoClinic.Profiles.Application.Dto.Patient;

/// <summary>
/// Data transfer object for filtering Patients
/// </summary>
public class PatientFilterDto
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
}