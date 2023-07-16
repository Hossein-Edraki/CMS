using Application.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUserAsync(UserDto user);
        Task<bool> ValidateUserAsync(string username, string password);
        Task<string> CreateTokenAsync();
    }
}
