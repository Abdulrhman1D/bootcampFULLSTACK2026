using ClinicManagementSystem.Domain.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User?> GetUserByUuidAsync(string uuid);

        Task<User?> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(string uuid);

        // -------------- //

        User? GetByUsername(string username);

        User? GetByEmail(string email);

        void Add(User user);
    }
}
