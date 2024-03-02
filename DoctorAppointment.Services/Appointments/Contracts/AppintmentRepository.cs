using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments.Contracts;
public interface AppointmentRepository
{
    void Add(Appointment appointment);
    void Update(Appointment appointment);


    Task<Appointment?> Find(int id);
    Task<List<AppointmentDto>?> Get();
    Task<List<AppointmentDto>?> GetBySpecific(Expression<Func<Appointment, bool>> where);

    

}
