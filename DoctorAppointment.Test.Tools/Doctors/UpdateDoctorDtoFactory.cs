using DoctorAppointment.Services.Doctors.Contracts.Dtos;

namespace DoctorAppointment.Test.Tools.Doctors;
public class UpdateDoctorDtoFactory
{

    public static UpdateDoctorDto Create(string? nationalCode = null)
    {
        return new UpdateDoctorDto
        {
            FirstName = "update-dummy-firstName",
            LastName = "update-dummy-lastName",
            Field = "update-heart",
            NationalCode = nationalCode ?? "update-123456789"
        };
    }
}
