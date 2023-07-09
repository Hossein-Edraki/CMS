using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(User user);
        Task<bool> ValidateUserAsync(string username, string password);
        Task<string> CreateTokenAsync();
    }
}
