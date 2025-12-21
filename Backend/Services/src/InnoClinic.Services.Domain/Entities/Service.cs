namespace InnoClinic.Services.Domain.Entities;

public class Service
{
    public Guid Id  { get; set; }
    public string ServiceName { get; set; }
    public int Price { get; set; }
    public bool IsActive { get; set; }
    public Guid CategoryId { get; set; }
    public ServiceCategory Category { get; set; }
    public Guid SpecializationId { get; set; }
    public Specialization Specialization { get; set; }
}