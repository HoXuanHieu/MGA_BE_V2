using Models.Entities;

namespace Repositories;

public interface IUserRepository
{
    public Task<List<UserEntity>> GetAllUsersAsync();
    public Task<UserEntity> CreateUserAsync(UserEntity user);
    public Task<String> DeleteUserAsync(String UserId);
    public Task<String> UpdateUserAsync(UserEntity user);
    public Task<UserEntity> GetUserByIdAsync(String userId);
}
