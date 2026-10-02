namespace ClinicManagementSystem.Models
{
    public class AppointmentViewModel
    {
        public Appointment Appointment { get; set; } = new Appointment();

        public List<Patient> Patients { get; set; } = new List<Patient>();

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
