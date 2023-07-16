using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string> CreateTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> RegisterUserAsync(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
