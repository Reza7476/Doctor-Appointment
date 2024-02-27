using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DoctorAppointment.Services.Unit.Tests.Doctors;
public class DoctorServiceTest
{
    [Fact]
    public async Task Add_adds_a_new_doctor_properly()
    {
        var dto = new AddDoctorDto
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            Field = "heart",
            NationalCode = "1234567890"
        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext =db. CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));


        await sut.Add(dto);

        var actual = readContext.Doctors.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.Field.Should().Be(dto.Field);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }
   
    [Fact]
    public async Task Add_throws_DoctorIsAlresdyExistedExceotion_when_existed()
    {
        var db=new EFInMemoryDatabase();    
        var readContext=db.CreateDataContext<EFDataContext>();
        var context = db.CreateDataContext<EFDataContext>();
        var dto = new AddDoctorDto()
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            Field = "heart",
            NationalCode = "1234567890"
        };
        var sut = new DoctorAppService(new EFDoctorRepository(readContext), new EFUnitOfWork(readContext));
        await sut.Add(dto);

        var actual = async () => await sut.Add(dto);

        await actual.Should().ThrowExactlyAsync<DoctorIsExsitException>();
    }
   
    [Fact]
    public async Task Update_updates_doctor_properly()
    {
        var db=new EFInMemoryDatabase();    
        var  context=db.CreateDataContext<EFDataContext>();
        var readContext=db.CreateDataContext<EFDataContext>();
        var doctor = new Doctor
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            Field = "heart",
            NationalCode = "1234567890",
        };
        context.Save(doctor);
        var sut=new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));

        var updateDoctorDto = new UpdateDoctorDto
        {
            FirstName = "update_dummy_Name",
            LastName = "updete_dummy_LastName",
            Field = "update_hesrt",
            NationalCode = "9876543210",
        };
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
        var db = new EFInMemoryDatabase();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut=new DoctorAppService(new EFDoctorRepository(readContext),new EFUnitOfWork(readContext));

        var update = new UpdateDoctorDto
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            Field = "heart",
            NationalCode = "1234567890"
        };
        var actual = async () => await sut.Update(5, update);

        await actual.Should().ThrowExactlyAsync<DoctorNotFoundException>();
    }

    [Fact]
    public async Task Delete_deletes_doctor_properly()
    {
        var db = new EFInMemoryDatabase();
        var context=db.CreateDataContext<EFDataContext>();
        var readCotext=db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var doctor = new Doctor
        {
            FirstName = "dummy_FirstName",
            LastName = "dummy_LastName",
            Field = "heart",
            NationalCode = "1234567890",
        };
        context.Save(doctor);

        await sut.Delete(doctor.Id);

        var actual=await readCotext.Doctors.FirstOrDefaultAsync(_=>_.Id==doctor.Id);
        actual.Should().BeNull();
    }
   
    [Fact]
    public async Task Delete_throws_DoctorNotFoundException_when_not_found()
    {
        var db = new EFInMemoryDatabase();
        var readContext=db.CreateDataContext<EFDataContext>();
        var sut=new DoctorAppService(new EFDoctorRepository(readContext), new EFUnitOfWork(readContext));

        var actual = async () => await sut.Delete(5);

        await actual.Should().ThrowExactlyAsync<DoctorNotFoundException>();
    }


}
