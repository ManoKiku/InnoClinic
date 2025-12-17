using InnoClinic.Profiles.Application.Data;
using InnoClinic.Profiles.Application.Dto.Doctor;
using InnoClinic.Profiles.Application.Dto.Receptionist;
using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Application.Services;

public class ReceptionistService : IReceptionistService
{
    private readonly ApplicationDbContext _context;

    public ReceptionistService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ReceptionistDto> GetByIdAsync(Guid receptionistId)
    {
        var receptionist = await _context.Receptionists
            .FirstOrDefaultAsync(p => p.Id == receptionistId);
        
        if (receptionist == null)
            throw new ArgumentException($"Receptionist with id {receptionistId} does not exist");
        
        return MapToDto(receptionist);
    }

    public async Task<ReceptionistDto> GetByAccountIdAsync(Guid accountId)
    {
        var receptionist = await _context.Receptionists
            .FirstOrDefaultAsync(p => p.AccountId == accountId);
        
        if (receptionist == null)
            throw new ArgumentException($"Receptionist with id {accountId} does not exist");
        
        return MapToDto(receptionist);
    }

    public async Task<IEnumerable<ReceptionistDto>> GetAllAsync(ReceptionistFilterDto filter)
    {
        var query = _context.Receptionists.AsQueryable();
        
        if(!string.IsNullOrEmpty(filter.FirstName))
            query = query.Where(p => p.FirstName.Contains(filter.FirstName));   
        
        if(!string.IsNullOrEmpty(filter.LastName))
            query = query.Where(p => p.LastName.Contains(filter.LastName)); 
        
        if(filter.OfficeId != null)
            query = query.Where(p => p.OfficeId == filter.OfficeId); 
        
        var receptionists = await query.ToListAsync();
        
        return receptionists.Select(MapToDto);
    }

    public async Task<ReceptionistDto> CreateAsync(CreateReceptionistDto receptionistDto)
    {
        var patient = new Receptionist
        {
            Id = Guid.NewGuid(),
            AccountId = receptionistDto.AccountId,
            FirstName = receptionistDto.FirstName,
            LastName = receptionistDto.LastName,
            MiddleName = receptionistDto.MiddleName,
            OfficeId = receptionistDto.OfficeId
        };
        
        _context.Receptionists.Add(patient);
        await _context.SaveChangesAsync();

        return MapToDto(patient);
    }

    public async Task<ReceptionistDto> UpdateAsync(Guid id, UpdateReceptionistDto receptionistDto)
    {
        var receptionist = await _context.Receptionists.FindAsync(id);
        if (receptionist == null)
            throw new KeyNotFoundException($"Receptionist with ID {id} not found");

        if (!string.IsNullOrEmpty(receptionistDto.FirstName))
            receptionist.FirstName = receptionistDto.FirstName;

        if (!string.IsNullOrEmpty(receptionistDto.LastName))
            receptionist.LastName = receptionistDto.LastName;

        if (receptionistDto.MiddleName != null)
            receptionist.MiddleName = receptionistDto.MiddleName;

        if (receptionistDto.OfficeId.HasValue)
            receptionist.OfficeId = receptionistDto.OfficeId.Value;

        await _context.SaveChangesAsync();

        return MapToDto(receptionist);
    }

    public async Task DeleteAsync(Guid id)
    {
        var receptionist = await _context.Receptionists.FindAsync(id);
        if (receptionist == null)
            throw new KeyNotFoundException($"Receptionist with ID {id} not found");

        _context.Receptionists.Remove(receptionist);
        await _context.SaveChangesAsync();
    }
    
    private static ReceptionistDto MapToDto(Receptionist receptionist)
    {
        return new ReceptionistDto()
        {
            Id = receptionist.Id,
            AccountId = receptionist.AccountId,
            FirstName = receptionist.FirstName,
            LastName = receptionist.LastName,
            MiddleName = receptionist.MiddleName,
            OfficeId = receptionist.OfficeId,
        };
    }
}