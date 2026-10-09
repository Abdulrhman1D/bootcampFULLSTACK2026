namespace ClinicManagementSystem.Dtos
{
    public class JobDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }

    public class JobCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class JobUpdateDto : JobCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
