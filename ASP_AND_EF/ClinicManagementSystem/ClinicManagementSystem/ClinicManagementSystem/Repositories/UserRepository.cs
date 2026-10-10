using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>>
            GetAllUsersAsync()
        {
            IEnumerable<User> users =
                await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User?>
            GetUserByUuidAsync(string uuid)
        {
            var user =
                await _context.Users.FirstOrDefaultAsync(
                    u => u.Uuid == uuid
                );

            return user;
        }

        public async Task<User?>
            GetUserByEmailAsync(string email)
        {
            var user =
                await _context.Users.FirstOrDefaultAsync(
                    u => u.Email == email
                );

            return user;
        }

        public Task AddUserAsync(User user)
        {
            _context.Users.Add(user);

            return _context.SaveChangesAsync();
        }

        public Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            return _context.SaveChangesAsync();
        }

        public Task DeleteUserAsync(string uuid)
        {
            var user = _context.Users.FirstOrDefault(
                u => u.Uuid == uuid
            );

            if (user != null)
            {
                _context.Users.Remove(user);

                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
