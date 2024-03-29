﻿using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments.Contracts;
public interface AppointmentService
{
    Task Add(AddAppointmentDto dto);
    Task Update(int id, UpdateAppointmentDto dto);

    Task Delete(int id);
}
