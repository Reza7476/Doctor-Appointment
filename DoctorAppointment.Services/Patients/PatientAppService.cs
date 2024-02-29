using DoctorAppointment.Contracts;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using DoctorAppointment.Services.Patients.Exceptions;

namespace DoctorAppointment.Services.Patients;
public class PatientAppService : PatientService
{

    private PatientRepository _patientRepository;
    private UnitOfWork _unitOfWork;

    public PatientAppService(PatientRepository patientRepository, UnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddPatientDto dto)
    {
        var patientExist = await _patientRepository.IsExist(dto.NationalCode);
        if (patientExist == true)
            throw new DuplicateNationalCodeException();
        var patient = new Patient()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            NationalCode = dto.NationalCode,
        };
        _patientRepository.Add(patient);
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var patient = await _patientRepository.FindById(id);
        if (patient == null)
            throw new PatientNotFoundException();
        _patientRepository.Delete(patient);
        await _unitOfWork.Complete();   
    }

    public async Task<List<PatientDto>?> Get()
    {
        return await _patientRepository.Get();
    }

    public async Task Update(int id, UpdatePatientDto dto)
    {
        var patientExist = await _patientRepository.FindById(id);
        if (patientExist == null)
            throw new PatientNotFoundException();
        patientExist.FirstName = dto.FirstName;
        patientExist.LastName = dto.LastName;
        patientExist.NationalCode = dto.NationalCode;
        _patientRepository.Update(patientExist);
        await _unitOfWork.Complete();
    }

}
