namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }
    }
}
