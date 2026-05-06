using AuthService.Entities;
namespace AuthService.Repositories;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId);
    Task<bool> ExistsByEmailAsync(string email);
    Task<List<User>> GetAllAsync();

    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid userId);
}