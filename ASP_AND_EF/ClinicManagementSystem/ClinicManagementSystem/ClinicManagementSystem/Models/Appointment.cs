using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Appointment
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; }

        public string Notes { get; set; }
    }
}
