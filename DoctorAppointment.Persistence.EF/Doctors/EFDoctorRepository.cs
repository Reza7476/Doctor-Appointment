using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;

namespace DoctorAppointment.Persistence.EF.Doctors;
public class EFDoctorRepository : DoctorRepository
{


    private readonly EFDataContext _db;

    public EFDoctorRepository(EFDataContext db)
    {
        _db = db;
    }

    public void Add(Doctor doctor)
    {
        _db.Set<Doctor>().Add(doctor);

    }
}
