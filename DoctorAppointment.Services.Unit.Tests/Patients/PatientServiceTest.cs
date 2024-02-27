using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Patients;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using DoctorAppointment.Services.Patients.Exceptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace DoctorAppointment.Services.Unit.Tests.Patients;
public class PatientServiceTest
{

    [Fact]
    public async Task Add_adds_new_patient_properly()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
        var dto = new AddPatientDto
        {
            FirstName = "dummy-FirstName",
            LastName = "dummy_LastName",
            NationalCode = "1234567890"
        };

        await sut.Add(dto);

        var actual = readContext.Patients.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }

    [Fact]
    public async Task Add_throws_PatientAlresdyExistedExceotion_when_existed()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
        var dto = new AddPatientDto
        {
            FirstName = "dummy-FirstName",
            LastName = "dummy_LastName",
            NationalCode = "1234567890"
        };
        await sut.Add(dto);

        var actual = async () => await sut.Add(dto);

        await actual.Should().ThrowExactlyAsync<PatientAlreadyExistedException>();
    }

    [Fact]
    public async Task Upadate_updates_patient_peroprly()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
        var patient = new Patient
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            NationalCode = "1234567890"
        };
        context.Save(patient);

        var dto = new UpdatePatientDto
        {
            FirstName = "update_dummy_FirstName",
            LastName = "update_dummy_LastName",
            NationalCode = "update_1234567890"
        };
        await sut.Update(patient.Id, dto);

        var actual = readContext.Patients.First(_ => _.Id == patient.Id);
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.NationalCode.Should().Be(dto.NationalCode);

    }
    [Fact]
    public async Task Update_throws_PatientNotFoundException_when_notfound()
    {
        var db = new EFInMemoryDatabase();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new PatientAppService(new EFPatientRepository(readContext), new EFUnitOfWork(readContext));

        var dto = new UpdatePatientDto
        {
            FirstName = "update_dummy_FirstName",
            LastName = "update_dummy_LastName",
            NationalCode = "update_1234567890"
        };
        var actual = async () => await sut.Update(5, dto);

       await actual.Should().ThrowExactlyAsync<PatientNotFoundException>();
    }

    [Fact]
    public async Task Delete_deletes_patient_properly()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
        var patient = new Patient
        {
            FirstName = "dummy-FirstName",
            LastName = "dummy_LastName",
            NationalCode = "123456789"
        };
        context.Save(patient);

        await sut.Delete(patient.Id);

        var actual =await readContext.Patients.FirstOrDefaultAsync(_ => _.Id == patient.Id);
        actual.Should().BeNull();
    }
    [Fact]
    public async Task Delete_throws_PatientNotFoundException_when_notFound()
    {
        var db = new EFInMemoryDatabase();
        var readContext=db.CreateDataContext<EFDataContext>();
        var sut = new PatientAppService(new EFPatientRepository(readContext), new EFUnitOfWork(readContext));

        var actual = async () => await sut.Delete(5);

        await actual.Should().ThrowExactlyAsync<PatientNotFoundException>();    
    }
}

