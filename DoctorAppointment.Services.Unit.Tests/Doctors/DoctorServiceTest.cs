using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
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

        var actual = readContext.Set<Doctor>().Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.Field.Should().Be(dto.Field);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }
}






