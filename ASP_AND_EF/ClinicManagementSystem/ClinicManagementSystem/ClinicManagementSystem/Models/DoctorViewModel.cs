namespace ClinicManagementSystem.Models
{
    public class DoctorViewModel
    {
        public Doctor Doctor { get; set; } = new Doctor();

        public List<Specialty> Specialties { get; set; }
            = new List<Specialty>();
    }
}
