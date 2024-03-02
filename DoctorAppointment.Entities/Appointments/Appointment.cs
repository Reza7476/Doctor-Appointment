using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Entities.Appointments;
public class Appointment
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
 
    
    public Status status { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public List<Patient> Patients { get; set; }
    public List<Doctor> Doctors { get; set; }
}
public enum Status
{
    wating,
    visiting,
    visited,
    cancel,
}
