using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AppointmentsDto
    {
        public short Id { get; set; }

        public string? Patient { get; set; }

        public short? Doctor { get; set; }

        public short? Medicine { get; set; }

        public DateOnly? AppointmentDate { get; set; }

        public string? DoctorName { get; set; }

        public String? MedicineName { get; set; }

        public string? PatientName { get; set; }

    }
}
