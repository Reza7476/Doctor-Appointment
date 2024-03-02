using DoctorAppointment.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF.Appointments;
public class AppointmentEntityMap : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd();
        builder.Property(_ => _.DoctorId).IsRequired();
        builder.Property(_ => _.PatientId).IsRequired();
        builder.Property(_ => _.DateTime).IsRequired();
        builder.Property(_ => _.Start).IsRequired();
        builder.Property(_ => _.End).IsRequired();
        builder.Property(_ => _.status).IsRequired();
    }
}
