using System;
using System.Collections.Generic;

namespace Dal_Repository.models;

public partial class Medicine
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public short? Dosage { get; set; }

    public short? Duration { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<AppointmentsArchiv> AppointmentsArchivs { get; set; } = new List<AppointmentsArchiv>();
}
