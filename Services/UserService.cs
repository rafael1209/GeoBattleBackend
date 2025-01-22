using GeoBattleBackend.Interfaces;
using GeoBattleBackend.Models;
using Google.Apis.Auth;

namespace GeoBattleBackend.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetByEmailAsync(email);

            return user;
        }

        public async Task<User?> CreateUserByPayload(GoogleJsonWebSignature.Payload googlePayload)
        {
            var user = new User
            {
                Email = googlePayload.Email,
                CreatedAtUtc = DateTime.UtcNow,
                AuthToken = Guid.NewGuid().ToString()
            };

            return await userRepository.CreateAsync(user);
        }
    }
}
