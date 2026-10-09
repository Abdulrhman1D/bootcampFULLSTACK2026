namespace ClinicManagementSystem.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;
    }

    public class PatientCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;
    }

    public class PatientUpdateDto : PatientCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
