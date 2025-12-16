using InnoClinic.Profiles.Application.Data;
using InnoClinic.Profiles.Application.Dto.Doctor;
using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly ApplicationDbContext _context;

    public DoctorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorDto> GetByIdAsync(Guid id)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id);

        if (doctor == null)
            throw new KeyNotFoundException($"Doctor with ID {id} not found");

        return MapToDto(doctor);
    }

    public async Task<DoctorDto> GetByAccountIdAsync(Guid accountId)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.AccountId == accountId);

        if (doctor == null)
            throw new KeyNotFoundException($"Doctor with Account ID {accountId} not found");

        return MapToDto(doctor);
    }

    public async Task<IEnumerable<DoctorDto>> GetAllAsync(DoctorFilterDto filter)
    {
        var query = _context.Doctors.AsQueryable();

        if (!string.IsNullOrEmpty(filter.FirstName))
            query = query.Where(d => d.FirstName.Contains(filter.FirstName));

        if (!string.IsNullOrEmpty(filter.LastName))
            query = query.Where(d => d.LastName.Contains(filter.LastName));

        if (filter.SpecializationId.HasValue)
            query = query.Where(d => d.SpecializationId == filter.SpecializationId.Value);

        if (filter.OfficeId.HasValue)
            query = query.Where(d => d.OfficeId == filter.OfficeId.Value);

        if (!string.IsNullOrEmpty(filter.Status))
            query = query.Where(d => d.Status.ToString() == filter.Status);

        var doctors = await query.ToListAsync();

        return doctors.Select(MapToDto);
    }

    public async Task<DoctorDto> CreateAsync(CreateDoctorDto doctorDto)
    {
        var doctor = new Doctor
        {
            Id = Guid.NewGuid(), 
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            MiddleName = doctorDto.MiddleName,
            AccountId = doctorDto.AccountId,
            DateOfBirth = doctorDto.DateOfBirth,
            SpecializationId = doctorDto.SpecializationId,
            OfficeId = doctorDto.OfficeId,
            CareerStartYear = doctorDto.CareerStartYear
        };
        
        doctor.Status = DoctorStatus.Inactive;

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return MapToDto(doctor);
    }

    public async Task<DoctorDto> UpdateAsync(Guid id, UpdateDoctorDto doctorDto)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
            throw new KeyNotFoundException($"Doctor with ID {id} not found");

        if (!string.IsNullOrEmpty(doctorDto.FirstName))
            doctor.FirstName = doctorDto.FirstName;

        if (!string.IsNullOrEmpty(doctorDto.LastName))
            doctor.LastName = doctorDto.LastName;

        if (doctorDto.MiddleName != null)
            doctor.MiddleName = doctorDto.MiddleName;

        if (doctorDto.DateOfBirth.HasValue)
            doctor.DateOfBirth = doctorDto.DateOfBirth.Value;

        if (doctorDto.SpecializationId.HasValue)
            doctor.SpecializationId = doctorDto.SpecializationId.Value;

        if (doctorDto.OfficeId.HasValue)
            doctor.OfficeId = doctorDto.OfficeId.Value;

        if (doctorDto.CareerStartYear.HasValue)
            doctor.CareerStartYear = doctorDto.CareerStartYear.Value;

        if (!string.IsNullOrEmpty(doctorDto.Status) && Enum.TryParse<DoctorStatus>(doctorDto.Status, out var status))
            doctor.Status = status;

        await _context.SaveChangesAsync();

        return MapToDto(doctor);
    }

    public async Task DeleteAsync(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
            throw new KeyNotFoundException($"Doctor with ID {id} not found");

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<DoctorDto> ChangeStatusAsync(Guid id, string status)
    {
        if (!Enum.TryParse<DoctorStatus>(status, out var doctorStatus))
            throw new ArgumentException($"Invalid status value: {status}");

        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
            throw new KeyNotFoundException($"Doctor with ID {id} not found");

        doctor.Status = doctorStatus;
        await _context.SaveChangesAsync();

        return MapToDto(doctor);
    }
    
    private  static  DoctorDto MapToDto(Doctor doctor)
    {
        return new DoctorDto()
        {
            Id = doctor.Id,
            AccountId = doctor.AccountId,
            CareerStartYear = doctor.CareerStartYear,
            DateOfBirth = doctor.DateOfBirth,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            MiddleName = doctor.MiddleName,
            OfficeId = doctor.OfficeId,
            SpecializationId = doctor.SpecializationId,
            Status = doctor.Status.ToString()
        };
    }
}