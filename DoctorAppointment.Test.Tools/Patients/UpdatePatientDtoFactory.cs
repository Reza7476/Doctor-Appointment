using DoctorAppointment.Services.Patients.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Patients;
public class UpdatePatientDtoFactory
{
   public static UpdatePatientDto  Create(string? nationalCode = null)
    {
        return new UpdatePatientDto
        {
            FirstName = "update_dummy_FirstName",
            LastName = "update_dummy_LastName",
            NationalCode = nationalCode ?? "update_1234567890"
        };
    }

}
