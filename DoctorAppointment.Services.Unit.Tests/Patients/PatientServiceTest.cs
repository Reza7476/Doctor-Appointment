using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Patients;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using DoctorAppointment.Services.Patients.Exceptions;
using DoctorAppointment.Test.Tools.Entities.Patients;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoctorAppointment.Services.Unit.Tests.Patients;
public class PatientServiceTest
{

    private readonly EFDataContext context;
    private readonly EFDataContext readContext;
    private readonly PatientService sut;
    public PatientServiceTest()
    {
        var db = new EFInMemoryDatabase();
        context = db.CreateDataContext<EFDataContext>();
        readContext = db.CreateDataContext<EFDataContext>();
        sut = PatientServiceFactory.Create(context);
    }

    [Fact]
    public async Task Add_adds_new_patient_properly()
    {

        var dto = AddPatientDtoFactory.Create();

        await sut.Add(dto);

        var actual = readContext.Patients.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }

    [Fact]
    public async Task Add_throws_DuplicateNationalCodeExceotion_when_existed()
    {
        var nationalCode = "596214744";
        var patient = new PatientServiceBuilder()
            .WithNationalCode(nationalCode)
            .Build();
        context.Save(patient);
        var dto = AddPatientDtoFactory.Create(nationalCode);

        var actual = async () => await sut.Add(dto);

        await actual.Should().ThrowExactlyAsync<DuplicateNationalCodeException>();
    }

    [Fact]
    public async Task Upadate_updates_patient_peroprly()
    {

        var patient = new PatientServiceBuilder()
            .Build();
        context.Save(patient);
        var dto = UpdatePatientDtoFactory.Create();

        await sut.Update(patient.Id, dto);

        var actual = readContext.Patients.First(_ => _.Id == patient.Id);
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.NationalCode.Should().Be(dto.NationalCode);

    }
    [Fact]
    public async Task Update_throws_PatientNotFoundException_when_notfound()
    {
        var id = 6666666;
        var dto = UpdatePatientDtoFactory.Create();
        var actual = async () => await sut.Update(id, dto);

        await actual.Should().ThrowExactlyAsync<PatientNotFoundException>();
    }

    [Fact]
    public async Task Delete_deletes_patient_properly()
    {
        var patient = new PatientServiceBuilder().Build();
        context.Save(patient);

        await sut.Delete(patient.Id);

        var actual = await readContext.Patients.FirstOrDefaultAsync(_ => _.Id == patient.Id);
        actual.Should().BeNull();
    }
    [Fact]
    public async Task Delete_throws_PatientNotFoundException_when_notFound()
    {
        var id = 666666666;

        var actual = async () => await sut.Delete(id);

        await actual.Should().ThrowExactlyAsync<PatientNotFoundException>();
    }

    [Fact]
    public async Task Get_returns_counts_of_saved_patients()
    {
        var nationalCode = "1236589749";
        var patient = new PatientServiceBuilder()
            .Build();
        context.Save(patient);
        var patient2 = new PatientServiceBuilder()
            .WithNationalCode(nationalCode)
            .Build();
        context.Save(patient2);

        await sut.Get();

        var actual = readContext.Patients;
        actual.Count().Should().Be(2);

    }


    [Fact]
    public async Task Get_gets_patient()
    {
        var nationalCode = "258963147";
        var patient = new PatientServiceBuilder().Build();
        context.Save(patient);
        var patient2 = new PatientServiceBuilder()
            .WithNationalCode(nationalCode)
            .Build();
        context.Save(patient2);
     
        var actual = await sut.Get();

        actual[0].FirstName.Should().Be(patient.FirstName);
        actual[0].LastName.Should().Be(patient.LastName);
        actual[0].NationalCode.Should().Be(patient.NationalCode);
        actual[1].FirstName.Should().Be(patient2.FirstName);
        actual[1].LastName.Should().Be(patient2.LastName);
        actual[1].NationalCode.Should().Be(patient2.NationalCode);
    }
}
