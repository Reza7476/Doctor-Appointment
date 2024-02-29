using DoctorAppointment.Services.Patients.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Patients;
public  class AddPatientDtoFactory
{
    public static AddPatientDto Create(string? nationalCode = null)
    {
        return new AddPatientDto
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            NationalCode = nationalCode ?? "1234567890"
        };
    }

}
