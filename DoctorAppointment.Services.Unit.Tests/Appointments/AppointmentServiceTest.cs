using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using DoctorAppointment.Services.Appointments.Exceptions;
using DoctorAppointment.Test.Tools.Entities.Appointments;
using DoctorAppointment.Test.Tools.Entities.Doctors;
using DoctorAppointment.Test.Tools.Entities.Patients;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace DoctorAppointment.Services.Unit.Tests.Appointments;
public class AppointmentServiceTest
{
    private readonly EFDataContext context;
    private readonly EFDataContext readContext;
    private readonly AppointmentService sut;
    public AppointmentServiceTest()
    {
        var db = new EFInMemoryDatabase();
        context = db.CreateDataContext<EFDataContext>();
        readContext = db.CreateDataContext<EFDataContext>();
        sut = AppointmentServiceFactory.Create(context);
    }

    [Fact]
    public async Task Add_addS_new_appointment_properly()
    {
        var doctor = new DoctorServiceBuilder().Build();
        context.Save(doctor);
        var patient = new PatientServiceBuilder().Build();
        context.Save(patient);
        var dto = AddAppointmentDtoFactory.Create(doctor.Id, patient.Id);

        await sut.Add(dto);

        var actual = readContext.Appointments.Single();
        actual.DoctorId = dto.DoctorId;
        actual.PatientId = dto.PatientId;
        actual.DateTime = dto.DateTime;
        actual.Start = dto.Start;
        actual.End = dto.End;
        actual.status = dto.status;
    }

    [Fact]
    public async Task Add_throws_FullTimeException_when_doctor_not_have_time_at_a_day()
    {
        var doctor = new DoctorServiceBuilder().Build();
        context.Save(doctor);
        var patient1 = new PatientServiceBuilder().Build();
        context.Save(patient1);
        var appointment1 = new AppointmentServiceBuilder()
            .WithStart("2024-02-03 07:00:52,555")
            .WithPatientId(patient1.Id)
            .WithDoctorId(doctor.Id)
            .Build();
        context.Save(appointment1);
        var patient2 = new PatientServiceBuilder()
            .WithNationalCode("258963147")
            .Build();
        context.Save(patient2);
        var appointment2 = new AppointmentServiceBuilder()
            .WithStart("2024-02-03 07:30:52,555")
            .WithPatientId(patient2.Id)
            .WithDoctorId(doctor.Id)
            .Build();
        context.Save(appointment2);
        var patient3 = new PatientServiceBuilder()
            .WithNationalCode("78965441233")
            .Build();
        context.Save(patient3);
        var appointment3 = new AppointmentServiceBuilder()
            .WithStart("2024-02-03 08:30:52,555")
            .WithPatientId(patient3.Id)
            .WithDoctorId(doctor.Id)
            .Build();
        context.Save(appointment3);
        var patient4 = new PatientServiceBuilder()
            .WithNationalCode("8532698745")
            .Build();
        context.Save(patient4);
        var appointment4 = new AppointmentServiceBuilder()
            .WithStart("2024-02-03 09:00:52,555")
            .WithPatientId(patient4.Id)
            .WithDoctorId(doctor.Id)
            .Build();
        context.Save(appointment4);
        var patient5 = new PatientServiceBuilder()
            .WithNationalCode("589658745896")
            .Build();
        context.Save(patient5);
        var appointment5 = new AppointmentServiceBuilder()
             .WithStart("2024-02-03 09:30:52,555")
             .WithPatientId(patient5.Id)
            .WithDoctorId(doctor.Id)
             .Build();
        context.Save(appointment5);
        var dto = AddAppointmentDtoFactory.Create(doctor.Id, patient1.Id);

        var actual = async () => await sut.Add(dto);

        await actual.Should().ThrowExactlyAsync<FullTimeException>();
    }

    [Fact]
    public async Task Add_hrows_TurnInterferenceException_when_interference_happend()
    {

        var startVisit = "2024-02-03 09:30:52,555";
        var doctor = new DoctorServiceBuilder().Build();
        context.Save(doctor);
        var patient = new PatientServiceBuilder().Build();
        context.Save(patient);
        var appointment = new AppointmentServiceBuilder()
            .WithStart(startVisit)
            .WithDoctorId(doctor.Id)
            .WithPatientId(patient.Id)
            .Build();
        context.Save(appointment);
        var dto = AddAppointmentDtoFactory.Create(doctor.Id, patient.Id, startVisit);

        var actual=async()=>await sut.Add(dto);

        await actual.Should().ThrowExactlyAsync<TurnInterferenceException>();
    }

    [Fact]
    public async Task Update_updates_appiontment_properly()
    {
        var startVisit = "2024-02-03 09:30:52,555";
        var doctor=new DoctorServiceBuilder().Build();
        context.Save(doctor);
        var patient=new PatientServiceBuilder().Build();
        context.Save(patient);
        var appointment = new AppointmentServiceBuilder().Build();
        context.Save(appointment);
        var updateDto = UpdateAppointmentDtoFActory.Create(doctor.Id, patient.Id, startVisit);

        await sut.Update(appointment.Id,updateDto);

        var actual=readContext.Appointments.First(_=>_.Id== appointment.Id);
        actual.DateTime .Should().Be( appointment.DateTime);
        actual.PatientId.Should().Be( patient.Id);
        actual.DoctorId.Should().Be(appointment.DoctorId);
        actual.Start.Should().Be(appointment.Start);
        actual.End.Should().Be(appointment.End);
    }

    [Fact]
    public async Task Update_throws_AppointmentNotFoundException_when_appointment_not_found()
    {
        var startVisit = "2024-02-03 09:30:52,555";
        var appointmentId = 22222222;
        var doctor = new DoctorServiceBuilder().Build();
        context.Save(doctor);
        var patient = new PatientServiceBuilder().Build();
        context.Save(patient);
        var appointment = new AppointmentServiceBuilder().Build();
        context.Save(appointment);
        var updateDto = UpdateAppointmentDtoFActory.Create(doctor.Id, patient.Id, startVisit);

        var actual=async()=>await sut.Update(appointmentId, updateDto);

        await actual.Should().ThrowExactlyAsync<AppointmentNotFoundException>();
    }

    

}
