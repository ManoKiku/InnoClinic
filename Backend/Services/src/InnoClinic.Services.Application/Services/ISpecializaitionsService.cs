using InnoClinic.Services.Application.Dto.Specialization;

namespace InnoClinic.Services.Application.Services;

public interface ISpecializationService
{
    Task<SpecializationDto> GetByIdAsync(Guid id);
    Task<IEnumerable<SpecializationDto>> GetAllAsync(SpecializationFilterDto filter);
    Task<SpecializationDto> CreateAsync(CreateSpecializationDto specializationDto);
    Task<SpecializationDto> UpdateAsync(Guid id, UpdateSpecializationDto specializationDto);
    Task<SpecializationDto> ChangeStatusAsync(Guid id, bool isActive);
    Task<bool> ExistsAsync(Guid id);
}