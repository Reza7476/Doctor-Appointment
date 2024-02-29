using DoctorAppointment.Services.Doctors.Contracts.Dtos;

namespace DoctorAppointment.Test.Tools.Doctors;
public class AddDoctorDtoFactory
{
    public static AddDoctorDto Create(string? nationalCode=null)
    {
        return new AddDoctorDto()
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            Field = "heart",
            NationalCode = nationalCode??"1234567890"
        };
    }
}
