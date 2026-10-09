using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Patient
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }
    }
}
