using InnoClinic.Services.Application.Dto.Services;

namespace InnoClinic.Services.Application.Services;

public interface IServicesService
{
    Task<ServiceDto> GetByIdAsync(Guid id);
    Task<IEnumerable<ServiceDto>> GetAllAsync(ServiceFilterDto filter);
    Task<ServiceDto> CreateAsync(CreateServiceDto serviceDto);
    Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceDto serviceDto);
    Task<ServiceDto> ChangeStatusAsync(Guid id, bool isActive);
    Task<IEnumerable<ServiceDto>> GetBySpecializationIdAsync(Guid specializationId);
    Task<IEnumerable<ServiceDto>> GetByCategoryIdAsync(Guid categoryId);
}