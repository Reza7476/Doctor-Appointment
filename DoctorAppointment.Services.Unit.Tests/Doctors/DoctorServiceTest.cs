using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Test.Tools.Doctors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoctorAppointment.Services.Unit.Tests.Doctors;
public class DoctorServiceTest
{
    private readonly EFDataContext context;
    private readonly EFDataContext readContext;
    private readonly DoctorService sut;
    public DoctorServiceTest()
    {
        var db = new EFInMemoryDatabase();
        context = db.CreateDataContext<EFDataContext>();
        readContext = db.CreateDataContext<EFDataContext>();
        sut = DoctorServiceFactory.Create(context);
    }

    [Fact]
    public async Task Add_adds_a_new_doctor_properly()
    {

        var dto = AddDoctorDtoFactory.Create();

        await sut.Add(dto);

        var actual = readContext.Doctors.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.Field.Should().Be(dto.Field);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }

    [Fact]
    public async Task Add_throws_DupicateNationalCodeExceotion_when_existed()
    {
        var nationalCode = "1234567890";
        var doctor = new DoctorServiceBuilder()
            .Build();
        context.Save(doctor);
        var dto = AddDoctorDtoFactory.Create(nationalCode);

        var actual = async () => await sut.Add(dto);

        await actual.Should().ThrowExactlyAsync<DupicateNationalCodeExceotion>();
    }

    [Fact]
    public async Task Update_updates_doctor_properly()
    {
        var doctor = new DoctorServiceBuilder().Build();
        context.Save(doctor);
        var updateDoctorDto = UpdateDoctorDtoFactory.Create();

        await sut.Update(doctor.Id, updateDoctorDto);

        var actual = readContext.Doctors.First(_ => _.Id == doctor.Id);
        actual.FirstName.Should().Be(updateDoctorDto.FirstName);
        actual.LastName.Should().Be(updateDoctorDto.LastName);
        actual.Field.Should().Be(updateDoctorDto.Field);
        actual.NationalCode.Should().Be(updateDoctorDto.NationalCode);
    }

    [Fact]
    public async Task Update_throws_DoctorNotFoundException_when_not_found()
    {
        var update = UpdateDoctorDtoFactory.Create();

        var actual = async () => await sut.Update(5, update);

        await actual.Should().ThrowExactlyAsync<DoctorNotFoundException>();
    }

    [Fact]
    public async Task Delete_deletes_doctor_properly()
    {
        var doctor = new DoctorServiceBuilder()
                    .Build();
        context.Save(doctor);

        await sut.Delete(doctor.Id);

        var actual = await readContext.Doctors
            .FirstOrDefaultAsync(_ => _.Id == doctor.Id);
        actual.Should().BeNull();
    }

    [Fact]
    public async Task Delete_throws_DoctorNotFoundException_when_not_found()
    {

        var actual = async () => await sut.Delete(5);

        await actual.Should().ThrowExactlyAsync<DoctorNotFoundException>();
    }

    [Fact]
    public async Task Get_returns_counts_of_saved_doctor()
    {
        var doctor = new DoctorServiceBuilder()
            .Build();
        var doctor2 = new DoctorServiceBuilder()
            .WithNationlCode("963325841")
            .Build();
        context.Save(doctor);
        context.Save(doctor2);

        await sut.Get();

        var actual = readContext.Doctors;
        actual.Count().Should().Be(2);

    }
    [Fact]

    public async Task Get_gets_doctors()
    {
        var doctor = new DoctorServiceBuilder()
            .Build();
        context.Save(doctor);
        var doctor2 = new DoctorServiceBuilder()
            .WithNationlCode("258963147")
            .Build();
        context.Save(doctor2);

        var actual = await sut.Get();

        actual[0].FirstName.Should().Be(doctor.FirstName);
        actual[0].LastName.Should().Be(doctor.LastName);
        actual[0].Field.Should().Be(doctor.Field);
        actual[0].NationalCode.Should().Be(doctor.NationalCode);
        actual[1].FirstName.Should().Be(doctor.FirstName);
        actual[1].LastName.Should().Be(doctor2.LastName);
        actual[1].Field.Should().Be(doctor2.Field);
        actual[1].NationalCode.Should().Be(doctor2.NationalCode);
    }



}
