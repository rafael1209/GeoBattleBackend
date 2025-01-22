using GeoBattleBackend.Models;
using Google.Apis.Auth;

namespace GeoBattleBackend.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> CreateUserByPayload(GoogleJsonWebSignature.Payload googlePayload);
    }
}
