using DoctorAppointment.Services.Doctors.Contracts.Dtos;

namespace DoctorAppointment.Services.Doctors.Contracts;
public interface DoctorService
{
    Task Add(AddDoctorDto dto);
}
