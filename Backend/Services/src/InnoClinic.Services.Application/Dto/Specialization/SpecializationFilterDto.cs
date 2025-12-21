namespace InnoClinic.Services.Application.Dto.Specialization;

/// <summary>
/// Data transfer object for filtering specializations
/// </summary>
public class SpecializationFilterDto
{
    /// <summary>
    /// Filter by specialization name (contains)
    /// </summary>
    /// <example>Cardio</example>
    public string SpecializationName { get; set; }

    /// <summary>
    /// Filter by active status
    /// </summary>
    /// <example>true</example>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Page number for pagination (starting from 1)
    /// </summary>
    /// <example>1</example>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size for pagination
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; set; } = 10;
}