using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Domain.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Job
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
    }
}
