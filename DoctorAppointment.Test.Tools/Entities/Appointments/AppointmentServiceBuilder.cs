using DoctorAppointment.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Entities.Appointments;
public class AppointmentServiceBuilder
{
    private readonly Appointment _appointment;

    public AppointmentServiceBuilder()
    {
        _appointment = new Appointment
        {
            DoctorId = 1,
            PatientId = 1,
            DateTime = DateTime.UtcNow,
            Start = DateTime
            .ParseExact("2024-02-03 09:00:52,555", "yyyy-MM-dd HH:mm:ss,fff",
                                      System.Globalization.CultureInfo.InvariantCulture),
            End = DateTime
            .ParseExact("2024-02-03 09:30:52,555", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
            status = Status.wating
        };
    }



    public AppointmentServiceBuilder WithStart(string start)
    {
        _appointment.Start = DateTime
            .ParseExact(start, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
        return this;
    }

    public AppointmentServiceBuilder WithDoctorId(int doctorId)
    {
        _appointment.DoctorId = doctorId;
        return this;
    }
    public AppointmentServiceBuilder WithPatientId(int patientId)
    {
        _appointment.PatientId = patientId;
        return this;
    }
    public Appointment Build()
    {
        return _appointment;
    }
}
