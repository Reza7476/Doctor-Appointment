using DoctorAppointment.Contracts;
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;

namespace DoctorAppointment.Services.Doctors;
public class DoctorAppService : DoctorService
{


    private readonly DoctorRepository _doctorRepository;
    private readonly UnitOfWork _unitOfWork;

    public DoctorAppService(DoctorRepository doctorRepository,
        UnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddDoctorDto dto)
    {
        var doctor = new Doctor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Field = dto.Field,
            NationalCode = dto.NationalCode
        };
        _doctorRepository.Add(doctor);
        await _unitOfWork.Complete();
    }
}
