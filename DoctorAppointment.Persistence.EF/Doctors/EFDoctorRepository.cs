using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.EF.Doctors;
public class EFDoctorRepository : DoctorRepository
{


    private readonly EFDataContext _db;

    public EFDoctorRepository(EFDataContext db)
    {
        _db = db;
    }

    public void Add(Doctor doctor)
    {
        _db.Doctors.Add(doctor);

    }

    public void Delete(Doctor doctor)
    {
        _db.Doctors.Remove(doctor);
    }

    public async Task<Doctor?> FindById(int id)
    {
        return await _db.Doctors.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<List<DoctorDto>?> Get()
    {
        return await _db.Doctors.Select(_ => new DoctorDto
        {
            FirstName=_.FirstName,
            LastName=_.LastName,
            Field=_.Field,
            NationalCode=_.NationalCode,
            Id = _.Id   
        }).ToListAsync();
    }

    public async Task<bool> IsExist(string NationalCode)
    {
        return await _db.Doctors.AnyAsync(_ => _.NationalCode == NationalCode);
    }
}
