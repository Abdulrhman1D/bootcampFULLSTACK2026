using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Specialty
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
    }
}
