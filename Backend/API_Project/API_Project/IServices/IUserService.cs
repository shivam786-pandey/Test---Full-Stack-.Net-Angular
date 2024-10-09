using API_Project.Entities;
using API_Project.RequestModel;
using OneOf;

namespace API_Project.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<OneOf<User, string>> GetUserByIdAsync(string userId);
        Task<OneOf<User, string>> CreateUserAsync(UserRequestModel user);
        Task<OneOf<User, string>> UpdateUserAsync(User user);
        Task<string> DeleteUserAsync(string userId);
    }
}
