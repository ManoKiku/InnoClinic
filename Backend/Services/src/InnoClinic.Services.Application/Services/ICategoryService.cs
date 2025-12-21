using InnoClinic.Services.Application.Dto.Categories;

namespace InnoClinic.Services.Application.Services;

public interface ICategoryService
{
    Task<ServiceCategoryDto> GetByIdAsync(Guid id);
    Task<IEnumerable<ServiceCategoryDto>> GetAllAsync();
    Task<ServiceCategoryDto> CreateAsync(CreateServiceCategoryDto categoryDto);
    Task<ServiceCategoryDto> UpdateAsync(Guid id, UpdateServiceCategoryDto categoryDto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}