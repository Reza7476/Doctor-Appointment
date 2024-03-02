using DoctorAppointment.Entities.Doctors;

namespace DoctorAppointment.Test.Tools.Entities.Doctors;
public class DoctorServiceBuilder
{
    private readonly Doctor _doctor;
    public DoctorServiceBuilder()
    {
        _doctor = new Doctor()
        {
            FirstName = "dummy-firstName",
            LastName = "dummy-lastName",
            Field = "heart",
            NationalCode = "1234567890"
        };
    }

    public DoctorServiceBuilder WithNationlCode(string nationlCode)
    {
        _doctor.NationalCode = nationlCode;
        return this;
    }

    public DoctorServiceBuilder WithFirstName(string firstName)
    {
        _doctor.FirstName = firstName;
        return this;
    }

    public DoctorServiceBuilder withLastName(string lastName)
    {
        _doctor.LastName = lastName;
        return this;
    }

    public DoctorServiceBuilder whithField(string field)
    {
        _doctor.Field = field;
        return this;
    }
    public Doctor Build()
    {
        return _doctor;
    }
}
