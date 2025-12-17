using InnoClinic.Offices.Application.Data;
using InnoClinic.Offices.Application.Dto;
using InnoClinic.Offices.Domain.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace InnoClinic.Offices.Application.Services;

public class OfficeService : IOfficeService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OfficeService> _logger;

    public OfficeService(ApplicationDbContext context, ILogger<OfficeService> logger)
    {
        _context = context;
        _logger = logger;
    }

    
    public async Task<IEnumerable<OfficeDto>> GetAllOfficesAsync(bool? isActive = null)
    {
        _logger.LogInformation("Retrieving all offices with filter: isActive={IsActive}", isActive);
        
        IQueryable<Office> query = _context.Offices.AsQueryable();

        if (isActive.HasValue)
        {
            query = query.Where(o => o.IsActive == isActive.Value);
        }

        var offices = await query.ToListAsync();
        _logger.LogInformation("Retrieved {Count} offices", offices.Count);
        
        return offices.Select(MapToDto);
    }

    
    public async Task<OfficeDto> GetOfficeByIdAsync(string id)
    {
        _logger.LogInformation("Retrieving office with ID: {Id}", id);
        
        var office = await _context.Offices.Find(id).FirstOrDefaultAsync();
        
        if (office == null)
        {
            _logger.LogWarning("Office with ID {Id} not found", id);
            throw new KeyNotFoundException($"Office with identifier {id} was not found");
        }

        _logger.LogInformation("Office with ID {Id} retrieved successfully", id);
        return MapToDto(office);
    }

    
    public async Task<OfficeDto> CreateOfficeAsync(CreateOfficeDto createOfficeDto)
    {
        _logger.LogInformation("Creating new office at address: {Address}", createOfficeDto.Address);
        
        var office = new Office
        {
            Address = createOfficeDto.Address,
            PhotoId = createOfficeDto.PhotoId,
            RegistryPhoneNumber = createOfficeDto.RegistryPhoneNumber,
            IsActive = createOfficeDto.IsActive
        };

        await _context.Offices.InsertOneAsync(office);

        _logger.LogInformation("Office created successfully with ID: {Id}", office.Id);
        return MapToDto(office);
    }

    
    public async Task<OfficeDto> UpdateOfficeAsync(string id, UpdateOfficeDto updateOfficeDto)
    {
        _logger.LogInformation("Updating office with ID: {Id}", id);
        
        var office = await _context.Offices.Find(id).FirstOrDefaultAsync();
        
        if (office == null)
        {
            _logger.LogWarning("Office with ID {Id} not found for update", id);
            throw new KeyNotFoundException($"Office with identifier {id} was not found");
        }

        if (!string.IsNullOrWhiteSpace(updateOfficeDto.Address))
        {
            office.Address = updateOfficeDto.Address;
        }
        
        if (updateOfficeDto.PhotoId.HasValue)
        {
            office.PhotoId = updateOfficeDto.PhotoId.Value;
        }
        
        if (!string.IsNullOrWhiteSpace(updateOfficeDto.RegistryPhoneNumber))
        {
            office.RegistryPhoneNumber = updateOfficeDto.RegistryPhoneNumber;
        }
        
        if (updateOfficeDto.IsActive.HasValue)
        {
            office.IsActive = updateOfficeDto.IsActive.Value;
        }

        await _context.Offices.FindOneAndReplaceAsync(office.Id, office);

        _logger.LogInformation("Office with ID {Id} updated successfully", id);
        return MapToDto(office);
    }

    
    public async Task<bool> DeleteOfficeAsync(string id)
    {
        _logger.LogInformation("Deleting office with ID: {Id}", id);
        
        var office = await _context.Offices.Find(id).FirstOrDefaultAsync();
        
        if (office == null)
        {
            _logger.LogWarning("Office with ID {Id} not found for deletion", id);
            throw new KeyNotFoundException($"Office with identifier {id} was not found");
        }

        await _context.Offices.FindOneAndDeleteAsync(office.Id);

        _logger.LogInformation("Office with ID {Id} deleted successfully", id);
        return true;
    }

    
    public async Task<OfficeDto> ActivateOfficeAsync(string id)
    {
        _logger.LogInformation("Activating office with ID: {Id}", id);
        
        var office = await _context.Offices.Find(id).FirstOrDefaultAsync();

        if (office == null)
        {
            _logger.LogWarning("Office with ID {Id} not found for activation", id);
            throw new KeyNotFoundException($"Office with identifier {id} was not found");
        }

        await _context.Offices.UpdateOneAsync(id, Builders<Office>.Update.Set(x => x.IsActive, true));

        _logger.LogInformation("Office with ID {Id} activated successfully", id);
        return MapToDto(office);
    }

    
    public async Task<OfficeDto> DeactivateOfficeAsync(string id)
    {
        _logger.LogInformation("Deactivating office with ID: {Id}", id);
        
        var office = await _context.Offices.Find(id).FirstOrDefaultAsync();
        
        if (office == null)
        {
            _logger.LogWarning("Office with ID {Id} not found for deactivation", id);
            throw new KeyNotFoundException($"Office with identifier {id} was not found");
        }

        await _context.Offices.UpdateOneAsync(id, Builders<Office>.Update.Set(x => x.IsActive, false));

        _logger.LogInformation("Office with ID {Id} deactivated successfully", id);
        return MapToDto(office);
    }
    
    private OfficeDto MapToDto(Office office)
    {
        return new OfficeDto
        {
            Id = office.Id,
            Address = office.Address,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            IsActive = office.IsActive
        };
    }
}