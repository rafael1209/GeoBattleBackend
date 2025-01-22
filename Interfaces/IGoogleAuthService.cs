using Google.Apis.Auth;

namespace GeoBattleBackend.Interfaces;

public interface IGoogleAuthService
{
    Task<Uri> GetGoogleAuthUrl();
    Task<GoogleJsonWebSignature.Payload?> HandleGoogleCallbackAsync(string code);
}