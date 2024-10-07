using System;
using System.Collections.Generic;

namespace Dal_Repository.models;

public partial class Patient
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public DateOnly? BirthDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<AppointmentsArchiv> AppointmentsArchivs { get; set; } = new List<AppointmentsArchiv>();
}
