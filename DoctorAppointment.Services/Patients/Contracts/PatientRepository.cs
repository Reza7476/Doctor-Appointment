using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts.Dtos;

namespace DoctorAppointment.Services.Patients.Contracts;
public interface PatientRepository
{
    Task<bool> IsExist(string nationalCode);
    
    void Add(Patient patient);
    void Update(Patient patient);
    void Delete(Patient patient);   
    
    Task<Patient?> FindById(int id);

    Task<List<PatientDto>?> Get();
}
