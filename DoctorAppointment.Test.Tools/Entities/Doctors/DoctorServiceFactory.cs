using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;

namespace DoctorAppointment.Test.Tools.Entities.Doctors;
public class DoctorServiceFactory
{
    public static DoctorService Create(EFDataContext contex)
    {
        var doctoRepository = new EFDoctorRepository(contex);
        var unitOfWork = new EFUnitOfWork(contex);
        return new DoctorAppService(doctoRepository, unitOfWork);

    }

}
