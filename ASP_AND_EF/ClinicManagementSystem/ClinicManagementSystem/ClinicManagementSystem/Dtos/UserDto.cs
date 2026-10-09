namespace ClinicManagementSystem.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsLocked { get; set; }
    }

    public class UserCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsLocked { get; set; }
    }

    public class UserUpdateDto
    {
        public string Uuid { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; }

        public bool IsLocked { get; set; }
    }
}
