namespace InnoClinic.Offices.Application.Dto;

using System;


/// <summary>
/// DTO for Office
/// </summary>
public class OfficeDto
{
    /// <summary>
    /// Office id
    /// <example>123e4567e89b12d3a4564266</example>
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Office address
    /// <example>Pushkin street, 25</example>
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// Photo id
    /// <example>223e4567-e89b-12d3-a456-426614174001</example>
    /// </summary>
    public Guid PhotoId { get; set; }
    
    /// <summary>
    /// Registry phone numbet
    /// <example>+375 29 539 21 21</example>
    /// </summary>
    public string RegistryPhoneNumber { get; set; }
    
    /// <summary>
    /// Status of office 
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; }
}
