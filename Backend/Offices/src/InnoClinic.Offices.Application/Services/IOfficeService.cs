using InnoClinic.Offices.Application.Dto;

namespace InnoClinic.Offices.Application.Services;

public interface IOfficeService
{
    Task<IEnumerable<OfficeDto>> GetAllOfficesAsync(bool? isActive = null);
    Task<OfficeDto> GetOfficeByIdAsync(string id);
    Task<OfficeDto> CreateOfficeAsync(CreateOfficeDto createOfficeDto);
    Task<OfficeDto> UpdateOfficeAsync(string id, UpdateOfficeDto updateOfficeDto);
    Task<bool> DeleteOfficeAsync(string id);
    Task<OfficeDto> ActivateOfficeAsync(string id);
    Task<OfficeDto> DeactivateOfficeAsync(string id);
}
