using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Dtos
{
    public class DoctorDto
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string SpecialtyName { get; set; } = string.Empty;

        public string JobName { get; set; } = string.Empty;

    }
    public class DoctorCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public int SpecialtyId { get; set; }
        public int? JobId { get; set; }
    }

    public class DoctorUpdateDto : DoctorCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
