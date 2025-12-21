namespace InnoClinic.Services.Application.Dto.Services;

/// <summary>
/// Data transfer object for filtering services
/// </summary>
public class ServiceFilterDto
{
    /// <summary>
    /// Filter by service name (contains)
    /// </summary>
    /// <example>Consultation</example>
    public string ServiceName { get; set; }

    /// <summary>
    /// Filter by category ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Filter by specialization ID
    /// </summary>
    /// <example>87654321-4321-4321-4321-210987654321</example>
    public Guid? SpecializationId { get; set; }

    /// <summary>
    /// Filter by status
    /// </summary>
    /// <example>true</example>
    public bool? Status { get; set; }

    /// <summary>
    /// Minimum price
    /// </summary>
    /// <example>0</example>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Maximum price
    /// </summary>
    /// <example>1000</example>
    public decimal? MaxPrice { get; set; }

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