using DoctorAppointment.Contracts;
using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using DoctorAppointment.Services.Appointments.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DoctorAppointment.Services.Appointments;
public class AppointmentAppService : AppointmentService
{


    private readonly AppointmentRepository _appointmentRepository;
    private readonly UnitOfWork _unitOfWork;
    public AppointmentAppService(AppointmentRepository appointmentRepository,
        UnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddAppointmentDto dto)
    {

        var docCountAppoint = await _appointmentRepository
            .GetBySpecific(x => x.DoctorId == dto.DoctorId &&
            x.DateTime.Day == dto.DateTime.Day &&
            x.status == Status.wating ||
            x.status == Status.visiting);
        if (docCountAppoint.Count >= 5)
            throw new FullTimeException();

        var patientAllowAppoint = await _appointmentRepository
            .GetBySpecific(x => x.DoctorId == dto.DoctorId &&
          x.Start == dto.Start &&
          x.status == Status.wating ||
          x.status == Status.visiting);
        if (patientAllowAppoint.Count >= 1)
            throw new TurnInterferenceException();

        var appointment = new Appointment
        {
            DoctorId = dto.DoctorId,
            PatientId = dto.PatientId,
            DateTime = dto.DateTime,
            Start = dto.Start,
            End = dto.End,
            status = dto.status
        };
        _appointmentRepository.Add(appointment);
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var appointment =await  _appointmentRepository.Find(id);
        if (appointment == null)
            throw new AppointmentNotFoundException();
        _appointmentRepository.Delete(appointment);
        await _unitOfWork.Complete();
    }

    public async Task Update(int id, UpdateAppointmentDto dto)
    {
        var appointment =await  _appointmentRepository.Find(id);
        if (appointment == null)
            throw new AppointmentNotFoundException();
        appointment.DateTime = dto.Date;
        appointment.Start = dto.Start;
        appointment.End= dto.End;
        appointment.status = dto.sataus;
        _appointmentRepository .Update(appointment);
        await _unitOfWork.Complete();
    }


}
