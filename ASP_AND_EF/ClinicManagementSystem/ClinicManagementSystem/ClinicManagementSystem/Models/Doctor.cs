using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Doctor
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Phone { get; set; }

        public int SpecialtyId { get; set; }

        public Specialty? Specialty { get; set; }

        public int? JobId { get; set; }
        public Job? Job { get; set; }
    }
}
