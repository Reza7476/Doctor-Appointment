using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Appointments;
using DoctorAppointment.Services.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Entities.Appointments;
public class AppointmentServiceFactory
{

    public static AppointmentService Create(EFDataContext context)
    {
        var appointmentRepository = new EFAppointmetRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        return new AppointmentAppService(appointmentRepository, unitOfWork);
    }
}
