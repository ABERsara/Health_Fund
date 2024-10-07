using System;
using System.Collections.Generic;

namespace Dal_Repository.models;

public partial class AppointmentsArchiv
{
    public short Id { get; set; }

    public string? Patient { get; set; }

    public short? Doctor { get; set; }

    public short? Medicine { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public virtual Doctor? DoctorNavigation { get; set; }

    public virtual Medicine? MedicineNavigation { get; set; }

    public virtual Patient? PatientNavigation { get; set; }
}
