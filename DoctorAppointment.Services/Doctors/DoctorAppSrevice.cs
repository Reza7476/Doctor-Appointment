using DoctorAppointment.Contracts;
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;
using DoctorAppointment.Services.Doctors.Exceptions;

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

        var doctorExist = await _doctorRepository.IsExist(dto.NationalCode);
        if (doctorExist == true)
            throw new DoctorIsExsitException();
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

    public async Task Delete(int id)
    {
        var doctor =await _doctorRepository.FindById(id);
        if(doctor == null)
            throw new DoctorNotFoundException();

        _doctorRepository.Delete(doctor);
        await _unitOfWork.Complete();   
    }


    public async Task Update(int id, UpdateDoctorDto dto)
    {
        var doctor = await _doctorRepository.FindById(id);
        if (doctor == null)
            throw new DoctorNotFoundException();
        doctor.FirstName = dto.FirstName;
        doctor.LastName = dto.LastName;
        doctor.Field = dto.Field;
        doctor.NationalCode = dto.NationalCode;
        await _unitOfWork.Complete();

    }

   

}
