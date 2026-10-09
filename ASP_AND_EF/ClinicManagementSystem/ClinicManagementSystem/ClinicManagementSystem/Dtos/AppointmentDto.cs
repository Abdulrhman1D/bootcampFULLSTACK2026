namespace ClinicManagementSystem.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class AppointmentCreateDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class AppointmentUpdateDto : AppointmentCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
