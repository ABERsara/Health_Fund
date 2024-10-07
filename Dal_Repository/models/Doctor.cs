using System;
using System.Collections.Generic;

namespace Dal_Repository.models;

public partial class Doctor
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public string? Specialty { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<AppointmentsArchiv> AppointmentsArchivs { get; set; } = new List<AppointmentsArchiv>();
}
