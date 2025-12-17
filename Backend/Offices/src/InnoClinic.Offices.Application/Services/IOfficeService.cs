using InnoClinic.Offices.Application.Dto;

namespace InnoClinic.Offices.Application.Services;

public interface IOfficeService
{
    Task<IEnumerable<OfficeDto>> GetAllOfficesAsync(bool? isActive = null);
    Task<OfficeDto> GetOfficeByIdAsync(Guid id);
    Task<OfficeDto> CreateOfficeAsync(CreateOfficeDto createOfficeDto);
    Task<OfficeDto> UpdateOfficeAsync(Guid id, UpdateOfficeDto updateOfficeDto);
    Task<bool> DeleteOfficeAsync(Guid id);
    Task<OfficeDto> ActivateOfficeAsync(Guid id);
    Task<OfficeDto> DeactivateOfficeAsync(Guid id);
}
