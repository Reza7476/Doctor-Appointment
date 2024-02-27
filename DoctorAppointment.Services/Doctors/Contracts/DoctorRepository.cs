using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;

namespace DoctorAppointment.Services.Doctors.Contracts;
public interface DoctorRepository
{

    void Add(Doctor doctor);
    void Delete(Doctor doctor);
    Task <Doctor?>FindById(int id);
    Task<bool> IsExist( string NationalCode);
   
}
