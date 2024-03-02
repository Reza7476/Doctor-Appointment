using DoctorAppointment.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments.Contracts.Dtos;
public class UpdateAppointmentDto
{
    public int DocId { get; set; }
    public int PatientId { get; set; }
    public DateTime Date { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Status sataus { get; set; }
}
