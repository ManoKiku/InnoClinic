using InnoClinic.Profiles.Application.Data;
using InnoClinic.Profiles.Application.Dto.Patient;
using InnoClinic.Profiles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Application.Services;

public class PatientService : IPatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<PatientDto> GetByIdAsync(Guid patientId)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Id == patientId);
        
        if (patient == null)
            throw new ArgumentException($"Patient with id {patientId} does not exist");
        
        return MapToDto(patient);
    }

    public async Task<PatientDto> GetByAccountIdAsync(Guid accountId)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.AccountId == accountId);
        
        if (patient == null)
            throw new ArgumentException($"Patient with account id {accountId} does not exist");
        
        return MapToDto(patient);
    }

    public async Task<IEnumerable<PatientDto>> GetAllAsync(PatientFilterDto filter)
    {
        var query = _context.Patients.AsQueryable();
        
        if(!string.IsNullOrEmpty(filter.FirstName))
            query = query.Where(p => p.FirstName.Contains(filter.FirstName));   
        
        if(!string.IsNullOrEmpty(filter.LastName))
            query = query.Where(p => p.LastName.Contains(filter.LastName)); 
        
        var patients = await query.ToListAsync();
        
        return patients.Select(MapToDto);
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto PatientDto)
    {
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            AccountId = PatientDto.AccountId,
            DateOfBirth = PatientDto.DateOfBirth,
            FirstName = PatientDto.FirstName,
            LastName = PatientDto.LastName,
            MiddleName = PatientDto.MiddleName
        };
        
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return MapToDto(patient);
    }

    public async Task<PatientDto> UpdateAsync(Guid id, UpdatePatientDto patientDto)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
            throw new KeyNotFoundException($"Patient with ID {id} not found");

        if (!string.IsNullOrEmpty(patientDto.FirstName))
            patient.FirstName = patientDto.FirstName;

        if (!string.IsNullOrEmpty(patientDto.LastName))
            patient.LastName = patientDto.LastName;

        if (patientDto.MiddleName != null)
            patient.MiddleName = patientDto.MiddleName;

        if (patientDto.DateOfBirth.HasValue)
            patient.DateOfBirth = patientDto.DateOfBirth.Value;

        await _context.SaveChangesAsync();

        return MapToDto(patient);
    }

    public async Task DeleteAsync(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
            throw new KeyNotFoundException($"Patient with ID {id} not found");

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
    
    private static PatientDto MapToDto(Patient patient)
    {
        return new PatientDto()
        {
            Id = patient.Id,
            AccountId = patient.AccountId,
            DateOfBirth = patient.DateOfBirth,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            MiddleName = patient.MiddleName,
            IsLinkedToAccount = patient.IsLinkedToAccount
        };
    }
}