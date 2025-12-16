using InnoClinic.Profiles.Application.Dto.Patient;

namespace InnoClinic.Profiles.Application.Services;

public interface IPatientService
{
    Task<PatientDto> GetByIdAsync(Guid patientId);
    Task<PatientDto> GetByAccountIdAsync(Guid accountId);
    Task<IEnumerable<PatientDto>> GetAllAsync(PatientFilterDto filter);
    Task<PatientDto> CreateAsync(CreatePatientDto patientDto);
    Task<PatientDto> UpdateAsync(Guid id, UpdatePatientDto patientDto);
    Task DeleteAsync(Guid id);
}