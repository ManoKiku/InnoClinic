namespace InnoClinic.Offices.Domain.Entities;

public class Office
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Address { get; set; }
    public Guid PhotoId { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public bool IsActive { get; set; }
}