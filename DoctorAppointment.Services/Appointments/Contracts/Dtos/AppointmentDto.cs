using DoctorAppointment.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments.Contracts.Dtos;
public  class AppointmentDto
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }


    public string status { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }


}
