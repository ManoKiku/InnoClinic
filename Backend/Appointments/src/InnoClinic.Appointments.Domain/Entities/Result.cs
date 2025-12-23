namespace InnoClinic.Appointments.Domain.Entities;

public class Result
{
    public Guid Id { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recomendations { get; set; }  
    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}