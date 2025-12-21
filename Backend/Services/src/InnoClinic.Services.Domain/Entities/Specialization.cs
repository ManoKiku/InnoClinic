namespace InnoClinic.Services.Domain.Entities;

public class Specialization
{
    public Guid Id { get; set; }
    public string SpecializationName { get; set; }
    public bool IsActive { get; set; }
    
    public IEnumerable<Service> Services { get; set; }
}