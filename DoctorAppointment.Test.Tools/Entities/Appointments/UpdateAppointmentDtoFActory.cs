using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Entities.Appointments;
public class UpdateAppointmentDtoFActory
{
    public static UpdateAppointmentDto Create(int docId,
        int patientId,
        string? start=null )
    {
        return new UpdateAppointmentDto()
        {
            DocId = docId,
            PatientId = patientId,
            Date = DateTime.Now,
            Start = DateTime
            .ParseExact(start = start ?? "2024-02-03 11:30:52,555", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture),
            End = DateTime
            .ParseExact("2024-02-03 11:30:52,555", "yyyy-MM-dd HH:mm:ss,fff",
                System.Globalization.CultureInfo.InvariantCulture),
            sataus = Status.wating
        };
    }
}
