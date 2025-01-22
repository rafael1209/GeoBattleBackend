using GeoBattleBackend.DataBase;
using GeoBattleBackend.Interfaces;
using GeoBattleBackend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;

namespace GeoBattleBackend.Repositories
{
    public class UserRepository(MongoDbContext context) : IUserRepository
    {
        private readonly IMongoCollection<User> _users = context.Users;

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(ObjectId id)
        {
            return await _users.Find(user => user.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByAuthTokenAsync(string authToken)
        {
            return await _users.Find(user => user.AuthToken == authToken)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(user => user.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);

            return user;
        }

        public async Task UpdateAsync(string id, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userID = ObjectId.Parse(id);

            var result = await _users.ReplaceOneAsync(u => u.Id == userID, user);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("User not found or no changes made.");
            }
        }

        public async Task DeleteAsync(string id)
        {
            var userID = ObjectId.Parse(id);

            var result = await _users.DeleteOneAsync(user => user.Id == userID);

            if (result.DeletedCount == 0)
            {
                throw new Exception("User not found.");
            }
        }
    }
}
