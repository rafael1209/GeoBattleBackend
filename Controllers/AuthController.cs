using GeoBattleBackend.Interfaces;
using GeoBattleBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoBattleBackend.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(IGoogleAuthService googleAuthService, IUserService userService) : Controller
    {
        [HttpGet("url")]
        public async Task<IActionResult> GetAuthUrl()
        {
            try
            {
                var url = await googleAuthService.GetGoogleAuthUrl();

                return Ok(new { url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error retrieving authorization URL.", details = ex.Message });
            }
        }

        [HttpGet("callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest(new { error = "Authorization code is required." });

            try
            {
                var result = await googleAuthService.HandleGoogleCallbackAsync(code);

                if (result == null)
                    return BadRequest(new { error = "Invalid authorization code or failed to authenticate." });

                var user = await userService.GetUserByEmailAsync(result.Email);

                if (user != null)
                {
                    return Ok(new { user });
                }

                var newUser = await userService.CreateUserByPayload(result);

                return Ok(new { newUser });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error processing authorization.", details = ex.Message });
            }
        }
    }
}
