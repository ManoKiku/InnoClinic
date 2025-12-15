using InnoClinic.Profiles.Application.Dto;

namespace InnoClinic.Profiles.Application.Services;

public interface IDoctorService
{
    Task<DoctorDto> GetByIdAsync(Guid id);
    Task<DoctorDto> GetByAccountIdAsync(Guid accountId);
    Task<IEnumerable<DoctorDto>> GetAllAsync(DoctorFilterDto filter);
    Task<DoctorDto> CreateAsync(CreateDoctorDto doctorDto);
    Task<DoctorDto> UpdateAsync(Guid id, UpdateDoctorDto doctorDto);
    Task DeleteAsync(Guid id);
    Task<DoctorDto> ChangeStatusAsync(Guid id, string status);
}