using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Entities.Appointments;
public class AddAppointmentDtoFactory
{
    public static AddAppointmentDto Create(int DocId, int PatinetId, string? start = null)
    {
        return new AddAppointmentDto()
        {
            DoctorId = DocId,
            PatientId = PatinetId,
            DateTime = DateTime.Now,
            Start = DateTime
            .ParseExact(start = start ?? "2024-02-03 11:30:52,555", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture),
            End = DateTime
            .ParseExact("2024-02-03 11:30:52,555", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture),
            status = Status.wating
        };
    }
}
