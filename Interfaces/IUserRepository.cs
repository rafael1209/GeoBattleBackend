using GeoBattleBackend.Models;
using MongoDB.Bson;

namespace GeoBattleBackend.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(ObjectId id);
    Task<User?> GetByAuthTokenAsync(string authToken);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> CreateAsync(User user);
    Task UpdateAsync(string id, User user);
    Task DeleteAsync(string id);
}