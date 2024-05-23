using Models.Entities;

namespace Repositories;

public interface IUserRepository
{
    public Task<List<UserEntity>> GetAllUsersAsync();
    public Task<Boolean> CreateUserAsync(UserEntity user);
}
