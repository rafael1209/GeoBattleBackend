using GeoBattleBackend.Interfaces;
using GeoBattleBackend.Models;
using MongoDB.Driver;

namespace GeoBattleBackend.DataBase
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        private const string ConstUsersCollection = "users";

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDb:ConnectionString"));
            _database = client.GetDatabase(configuration.GetValue<string>("MongoDb:Name"));
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(ConstUsersCollection);
    }
}
