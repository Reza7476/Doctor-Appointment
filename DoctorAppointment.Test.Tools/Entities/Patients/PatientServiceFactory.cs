using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Patients;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Entities.Patients;
public class PatientServiceFactory
{
    public static PatientService Create(EFDataContext context)
    {
        var patientRepository = new EFPatientRepository(context);
        var unitOfWork = new EFUnitOfWork(context);
        return new PatientAppService(patientRepository, unitOfWork);
    }

}
