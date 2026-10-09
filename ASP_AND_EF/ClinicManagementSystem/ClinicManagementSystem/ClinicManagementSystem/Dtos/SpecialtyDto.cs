namespace ClinicManagementSystem.Dtos
{
    public class SpecialtyDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }

    public class SpecialtyCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class SpecialtyUpdateDto : SpecialtyCreateDto
    {
        public string Uuid { get; set; } = string.Empty;
    }
}
