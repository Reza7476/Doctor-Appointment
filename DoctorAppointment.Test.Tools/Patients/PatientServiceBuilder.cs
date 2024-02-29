using DoctorAppointment.Entities.Patients;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Patients;
public class PatientServiceBuilder
{

    private readonly Patient _patient;

    public PatientServiceBuilder()
    {
        _patient = new Patient()
        {
            FirstName = "dummy-firstName",
            LastName = "dummy-lastName",
            NationalCode = "1234567890"
        };
    }


    public PatientServiceBuilder WithNationalCode(string nationalCode)
    {
        _patient.NationalCode = nationalCode;
        return this;

    }



    public PatientServiceBuilder WithFirstName(string firstName)
    {
        _patient.FirstName = firstName;
        return this;
    }



    public PatientServiceBuilder WithLastName(string lastName)
    {
        _patient.LastName = lastName;
        return this;
    }


    public Patient Build()
    {
        return _patient;
    }

}
