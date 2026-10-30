using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using ClinicManagementSystem.Services.Base;

namespace ClinicManagementSystem.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>>
            GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository
                .GetAllUsersAsync();

            return users.Select(MapUser);
        }

        public async Task<UserDto?>
            GetUserDetailsByUuidAsync(string uuid)
        {
            var user = await _unitOfWork.UserRepository
                .GetUserByUuidAsync(uuid);

            return user == null ? null : MapUser(user);
        }

        public async Task<UserUpdateDto?>
            GetUserByUuidAsync(string uuid)
        {
            var user = await _unitOfWork.UserRepository
                .GetUserByUuidAsync(uuid);

            if (user == null)
                return null;

            return new UserUpdateDto
            {
                Uuid = user.Uuid,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                IsLocked = user.IsLocked
            };
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _unitOfWork.UserRepository
                .GetUserByEmailAsync(email);
        }

        public async Task AddUserAsync(UserCreateDto userDto)
        {
            var existingUser =
                await GetUserByEmailAsync(userDto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException(
                    "This email is already registered."
                );
            }

            var user = new User
            {
                Name = userDto.Name,
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(
                    userDto.Password
                ),
                IsLocked = userDto.IsLocked
            };

            await _unitOfWork.UserRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(
            UserUpdateDto userDto)
        {
            var user = await _unitOfWork.UserRepository
                .GetUserByUuidAsync(userDto.Uuid);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            var existingUser =
                await GetUserByEmailAsync(userDto.Email);

            if (existingUser != null &&
                existingUser.Uuid != userDto.Uuid)
            {
                throw new InvalidOperationException(
                    "This email is already registered."
                );
            }

            user.Name = userDto.Name;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.IsLocked = userDto.IsLocked;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                user.Password =
                    BCrypt.Net.BCrypt.HashPassword(
                        userDto.Password
                    );
            }

            await _unitOfWork.UserRepository
                .UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(string uuid)
        {
            var user = await _unitOfWork.UserRepository
                .GetUserByUuidAsync(uuid);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            await _unitOfWork.UserRepository
                .DeleteUserAsync(uuid);
        }

        private static UserDto MapUser(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Uuid = user.Uuid,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                IsLocked = user.IsLocked
            };
        }

        // ------------------ // 

         public User? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepository.GetByUsername(loginDto.UserName);
            if (user == null )
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }


        public User? Register(RegisterDto registerDto)
        {
            var existingUserByUsername = _unitOfWork.UserRepository.GetByUsername(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return null;
            }
            var existingUserByEmail = _unitOfWork.UserRepository.GetByEmail(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return null;
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Name = registerDto.Name,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword
            };
            _unitOfWork.UserRepository.Add(newUser);
           
            return newUser;
        }

    }
}
