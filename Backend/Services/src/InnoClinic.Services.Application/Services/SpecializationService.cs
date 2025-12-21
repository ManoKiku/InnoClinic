using InnoClinic.Services.Application.Data;
using InnoClinic.Services.Application.Dto.Specialization;
using InnoClinic.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Services.Application.Services;

public class SpecializationService : ISpecializationService
{
    private readonly ApplicationDbContext _context;

    public SpecializationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SpecializationDto> GetByIdAsync(Guid id)
    {
        var specialization = await _context.Specializations
            .Include(s => s.Services)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (specialization == null)
            throw new KeyNotFoundException($"Specialization with ID {id} not found");

        var dto = MapToDto(specialization);
        dto.ServiceCount = specialization.Services?.Count() ?? 0;
        return dto;
    }

    public async Task<IEnumerable<SpecializationDto>> GetAllAsync(SpecializationFilterDto filter)
    {
        var query = _context.Specializations
            .Include(s => s.Services)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.SpecializationName))
            query = query.Where(s => s.SpecializationName.Contains(filter.SpecializationName));

        if (filter.IsActive.HasValue)
            query = query.Where(s => s.IsActive == filter.IsActive.Value);

        var specializations = await query
            .OrderBy(s => s.SpecializationName)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return specializations.Select(s =>
        {
            var dto = MapToDto(s);
            dto.ServiceCount = s.Services?.Count() ?? 0;
            return dto;
        });
    }

    public async Task<SpecializationDto> CreateAsync(CreateSpecializationDto specializationDto)
    {
        var specialization = new Specialization
        {
            Id = Guid.NewGuid(),
            SpecializationName = specializationDto.SpecializationName,
            IsActive = specializationDto.IsActive
        };
        
        _context.Specializations.Add(specialization);
        await _context.SaveChangesAsync();

        var dto = MapToDto(specialization);
        dto.ServiceCount = 0;
        return dto;
    }

    public async Task<SpecializationDto> UpdateAsync(Guid id, UpdateSpecializationDto specializationDto)
    {
        var specialization = await _context.Specializations
            .Include(s => s.Services)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (specialization == null)
            throw new KeyNotFoundException($"Specialization with ID {id} not found");

        if (!string.IsNullOrEmpty(specializationDto.SpecializationName))
            specialization.SpecializationName = specializationDto.SpecializationName;

        if (specializationDto.IsActive.HasValue)
            specialization.IsActive = specializationDto.IsActive.Value;

        await _context.SaveChangesAsync();

        var dto = MapToDto(specialization);
        dto.ServiceCount = specialization.Services?.Count() ?? 0;
        return dto;
    }

    public async Task<SpecializationDto> ChangeStatusAsync(Guid id, bool isActive)
    {
        var specialization = await _context.Specializations
            .Include(s => s.Services)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (specialization == null)
            throw new KeyNotFoundException($"Specialization with ID {id} not found");

        specialization.IsActive = isActive;
        await _context.SaveChangesAsync();

        var dto = MapToDto(specialization);
        dto.ServiceCount = specialization.Services?.Count() ?? 0;
        return dto;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Specializations.AnyAsync(s => s.Id == id);
    }

    private SpecializationDto MapToDto(Specialization specialization)
    {
        return new SpecializationDto
        {
            Id = specialization.Id,
            SpecializationName = specialization.SpecializationName,
            IsActive = specialization.IsActive
        };
    }
}