using GeoBattleBackend.Models;
using MongoDB.Driver;

namespace GeoBattleBackend.Interfaces;

public interface IMongoDbContext
{
    IMongoCollection<User> Users { get; }
}