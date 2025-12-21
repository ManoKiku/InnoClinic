using InnoClinic.Services.Application.Data;
using InnoClinic.Services.Application.Dto.Services;
using InnoClinic.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Services.Application.Services;

public class ServicesService : IServicesService
{
    private readonly ApplicationDbContext _context;

    public ServicesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceDto> GetByIdAsync(Guid id)
    {
        var service = await _context.Services
            .Include(s => s.Category)
            .Include(s => s.Specialization)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (service == null)
            throw new KeyNotFoundException($"Service with ID {id} not found");

        return MapToDto(service);
    }

    public async Task<IEnumerable<ServiceDto>> GetAllAsync(ServiceFilterDto filter)
    {
        var query = _context.Services
            .Include(s => s.Category)
            .Include(s => s.Specialization)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.ServiceName))
            query = query.Where(s => s.ServiceName.Contains(filter.ServiceName));

        if (filter.CategoryId.HasValue)
            query = query.Where(s => s.CategoryId == filter.CategoryId.Value);

        if (filter.SpecializationId.HasValue)
            query = query.Where(s => s.SpecializationId == filter.SpecializationId.Value);

        if (filter.Status != null)
            query = query.Where(s => s.IsActive == filter.Status);

        if (filter.MinPrice.HasValue)
            query = query.Where(s => s.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(s => s.Price <= filter.MaxPrice.Value);

        var services = await query
            .OrderBy(s => s.ServiceName)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return services.Select(MapToDto);
    }

    public async Task<ServiceDto> CreateAsync(CreateServiceDto serviceDto)
    {
        var categoryExists = await _context.ServiceCategories.AnyAsync(c => c.Id == serviceDto.CategoryId);
        if (!categoryExists)
            throw new ArgumentException($"Category with ID {serviceDto.CategoryId} not found");

        var specializationExists = await _context.Specializations.AnyAsync(s => s.Id == serviceDto.SpecializationId);
        if (!specializationExists)
            throw new ArgumentException($"Specialization with ID {serviceDto.SpecializationId} not found");

        var service = new Service
        {
            Id = Guid.NewGuid(),
            ServiceName = serviceDto.ServiceName,
            CategoryId = serviceDto.CategoryId,
            SpecializationId = serviceDto.SpecializationId,
            Price = serviceDto.Price,
            IsActive = serviceDto.Status
        };
        
        service.Id = Guid.NewGuid();

        _context.Services.Add(service);
        await _context.SaveChangesAsync();

        await _context.Entry(service)
            .Reference(s => s.Category)
            .LoadAsync();
        await _context.Entry(service)
            .Reference(s => s.Specialization)
            .LoadAsync();

        return MapToDto(service);
    }

    public async Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceDto serviceDto)
    {
        var service = await _context.Services
            .Include(s => s.Category)
            .Include(s => s.Specialization)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (service == null)
            throw new KeyNotFoundException($"Service with ID {id} not found");

        if (!string.IsNullOrEmpty(serviceDto.ServiceName))
            service.ServiceName = serviceDto.ServiceName;

        if (serviceDto.Price.HasValue)
            service.Price = serviceDto.Price.Value;

        if (serviceDto.CategoryId.HasValue)
        {
            var categoryExists = await _context.ServiceCategories.AnyAsync(c => c.Id == serviceDto.CategoryId.Value);
            if (!categoryExists)
                throw new ArgumentException($"Category with ID {serviceDto.CategoryId} not found");
            service.CategoryId = serviceDto.CategoryId.Value;
        }

        if (serviceDto.SpecializationId.HasValue)
        {
            var specializationExists = await _context.Specializations.AnyAsync(s => s.Id == serviceDto.SpecializationId.Value);
            if (!specializationExists)
                throw new ArgumentException($"Specialization with ID {serviceDto.SpecializationId} not found");
            service.SpecializationId = serviceDto.SpecializationId.Value;
        }
        
        service.IsActive = serviceDto.Status;

        await _context.SaveChangesAsync();

        return MapToDto(service);
    }

    public async Task<ServiceDto> ChangeStatusAsync(Guid id, bool isActive)
    {
        var service = await _context.Services
            .Include(s => s.Category)
            .Include(s => s.Specialization)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (service == null)
            throw new KeyNotFoundException($"Service with ID {id} not found");

        service.IsActive = isActive;
        await _context.SaveChangesAsync();

        return MapToDto(service);
    }

    public async Task<IEnumerable<ServiceDto>> GetBySpecializationIdAsync(Guid specializationId)
    {
        var services = await _context.Services
            .Include(s => s.Category)
            .Include(s => s.Specialization)
            .Where(s => s.SpecializationId == specializationId)
            .OrderBy(s => s.ServiceName)
            .ToListAsync();

        return services.Select(MapToDto);
    }

    public async Task<IEnumerable<ServiceDto>> GetByCategoryIdAsync(Guid categoryId)
    {
        var services = await _context.Services
            .Include(s => s.Category)
            .Include(s => s.Specialization)
            .Where(s => s.CategoryId == categoryId)
            .OrderBy(s => s.ServiceName)
            .ToListAsync();

        return services.Select(MapToDto);
    }

    private ServiceDto MapToDto(Service service)
    {
        return new ServiceDto
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            Price = service.Price,
            CategoryId = service.CategoryId,
            CategoryName = service.Category.CategoryName,
            SpecializationId = service.SpecializationId,
            SpecializationName = service.Specialization.SpecializationName,
            Status = service.IsActive ? "Active" : "Inactive",
            TimeSlotSize = service.Category.TimeSlotSize,
        };
    }
}