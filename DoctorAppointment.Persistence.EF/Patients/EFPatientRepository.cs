using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.EF.Patients;
public class EFPatientRepository:PatientRepository
{
    private readonly EFDataContext _db;

    public EFPatientRepository(EFDataContext db)
    {
        _db = db;
    }

    public void Add(Patient patient)
    {
       _db.Patients.Add(patient);
    }

    public void Delete(Patient patient)
    {
        _db.Patients.Remove(patient);
    }

    public async Task<Patient?> FindById(int id)
    {
        return await _db.Patients.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<bool> IsExist(string nationalCode)
    {
       return await _db.Patients.AnyAsync(_=>_.NationalCode == nationalCode);
    }

    public void Update(Patient patient)
    {
       _db.Update(patient);
    }
}
