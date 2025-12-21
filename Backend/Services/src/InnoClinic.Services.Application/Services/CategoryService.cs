using InnoClinic.Services.Application.Data;
using InnoClinic.Services.Application.Dto.Categories;
using InnoClinic.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Services.Application.Services;


public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceCategoryDto> GetByIdAsync(Guid id)
    {
        var category = await _context.ServiceCategories
            .Include(c => c.Services)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            throw new KeyNotFoundException($"Category with ID {id} not found");

        var dto = MapToDto(category);
        dto.ServiceCount = category.Services?.Count() ?? 0;
        return dto;
    }

    public async Task<IEnumerable<ServiceCategoryDto>> GetAllAsync()
    {
        var categories = await _context.ServiceCategories
            .Include(c => c.Services)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        return categories.Select(c =>
        {
            var dto = MapToDto(c);
            dto.ServiceCount = c.Services?.Count() ?? 0;
            return dto;
        });
    }

    public async Task<ServiceCategoryDto> CreateAsync(CreateServiceCategoryDto categoryDto)
    {
        var category = new ServiceCategory
        {
            Id = Guid.NewGuid(),
            CategoryName = categoryDto.CategoryName,
            TimeSlotSize = categoryDto.TimeSlotSize
        };

        _context.ServiceCategories.Add(category);
        await _context.SaveChangesAsync();

        var dto = MapToDto(category);
        dto.ServiceCount = 0;
        return dto;
    }

    public async Task<ServiceCategoryDto> UpdateAsync(Guid id, UpdateServiceCategoryDto categoryDto)
    {
        var category = await _context.ServiceCategories
            .Include(c => c.Services)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            throw new KeyNotFoundException($"Category with ID {id} not found");

        if (!string.IsNullOrEmpty(categoryDto.CategoryName))
            category.CategoryName = categoryDto.CategoryName;

        if (categoryDto.TimeSlotSize.HasValue)
            category.TimeSlotSize = categoryDto.TimeSlotSize.Value;

        await _context.SaveChangesAsync();

        var dto = MapToDto(category);
        dto.ServiceCount = category.Services?.Count() ?? 0;
        return dto;
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _context.ServiceCategories
            .Include(c => c.Services)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            throw new KeyNotFoundException($"Category with ID {id} not found");

        if (category.Services?.Any() == true)
            throw new InvalidOperationException("Cannot delete category with existing services");

        _context.ServiceCategories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.ServiceCategories.AnyAsync(c => c.Id == id);
    }

    private ServiceCategoryDto MapToDto(ServiceCategory serviceCategory)
    {
        return new ServiceCategoryDto
        {
            Id = serviceCategory.Id,
            CategoryName = serviceCategory.CategoryName,
            TimeSlotSize = serviceCategory.TimeSlotSize
        };
    }
}