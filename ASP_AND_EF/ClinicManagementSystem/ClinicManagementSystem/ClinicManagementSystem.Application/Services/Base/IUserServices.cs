using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;

namespace ClinicManagementSystem.Services.Base
{
    public interface IUserServices
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserDetailsByUuidAsync(string uuid);
        Task<UserUpdateDto?> GetUserByUuidAsync(string uuid);
        Task<User?> GetUserByEmailAsync(string email);

        Task AddUserAsync(UserCreateDto userDto);
        Task UpdateUserAsync(UserUpdateDto userDto);
        Task DeleteUserAsync(string uuid);

        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);
    }

    
}
