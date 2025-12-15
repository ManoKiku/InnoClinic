namespace InnoClinic.Offices.Application.Dto;

/// <summary>
/// DTO for error responses
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Error message
    /// <example>Office not found</example>
    /// </summary>
    public string Message { get; set; }
        
    /// <summary>
    /// Detailed error information (optional)
    /// <example>Office with specified identifier does not exist</example>
    /// </summary>
    public string Details { get; set; }
        
    /// <summary>
    /// Error timestamp
    /// <example>2024-01-15T10:30:00Z</example>
    /// </summary>
    public DateTime Timestamp { get; set; }
}