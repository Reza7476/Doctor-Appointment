using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF.Appointments;
public class EFAppointmetRepository : AppointmentRepository
{
    private readonly EFDataContext _db;

    public EFAppointmetRepository(EFDataContext db)
    {
        _db = db;
    }

    public void Add(Appointment appointment)
    {
        _db.Appointments.Add(appointment);
    }

    public async Task<Appointment?> Find(int id)
    {
       return await _db.Appointments.FirstOrDefaultAsync(x=> x.Id == id);
    }

    public async Task<List<AppointmentDto>?> Get()
    {
        return await _db.Appointments
            .Include(x => x.Patients)
            .Include(x => x.Doctors)
            .Select(_ => new AppointmentDto
            {
                DoctorId = _.DoctorId,
                PatientId = _.PatientId,
                DateTime = _.DateTime,
                End = _.End,
                Start = _.Start,
                status = _.status.ToString(),
                Id = _.Id
            }).ToListAsync();
    }

    public async Task<List<AppointmentDto>?> GetBySpecific(Expression<Func<Appointment, bool>> where)
    {
        var appointments = await _db.Appointments
            .Include(x => x.Doctors)
            .Include(x => x.Patients)
            .Where(where)
            .Select(_ => new AppointmentDto
            {
                DoctorId = _.DoctorId,
                PatientId = _.PatientId,
                DateTime = _.DateTime,
                End = _.End,
                Start = _.Start,
                status = _.status.ToString(),
                Id = _.Id
            }).ToListAsync();

        return appointments;
    }

    public  void Update(Appointment appointment)
    {
        _db.Appointments.Update(appointment);
    }
}
