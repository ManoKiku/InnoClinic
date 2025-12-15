using InnoClinic.Profiles.Domain.Enums;

namespace InnoClinic.Profiles.Domain.Entities;

public class Doctor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime  DateOfBirth { get; set; }
    public Guid AccountId { get; set; }
    public Guid SpecializationId { get; set; }
    public Guid OfficeId { get; set; }
    public int CareerStartYear { get; set; }
    public DoctorStatus Status { get; set; }

    public int Experience => DateTime.Now.Year - CareerStartYear + 1;
}