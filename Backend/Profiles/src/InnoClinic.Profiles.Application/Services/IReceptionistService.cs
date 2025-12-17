using InnoClinic.Profiles.Application.Dto.Doctor;
using InnoClinic.Profiles.Application.Dto.Receptionist;

namespace InnoClinic.Profiles.Application.Services;

public interface IReceptionistService
{
    Task<ReceptionistDto> GetByIdAsync(Guid receptionistId);
    Task<ReceptionistDto> GetByAccountIdAsync(Guid accountId);
    Task<IEnumerable<ReceptionistDto>> GetAllAsync(ReceptionistFilterDto filter);
    Task<ReceptionistDto> CreateAsync(CreateReceptionistDto receptionistDto);
    Task<ReceptionistDto> UpdateAsync(Guid id, UpdateReceptionistDto receptionistDto);
    Task DeleteAsync(Guid id);
}