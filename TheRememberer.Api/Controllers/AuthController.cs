using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Interfaces;

namespace TheRememberer.Api.Controllers
{
    [ApiVersion("1.0")]
    public class AuthController : DecoratedControllerBase<AuthController>
    {
        private readonly IConfiguration _config;
        private readonly IUserBiz _userBiz;
        public AuthController(ILogger<AuthController> logger, IConfiguration config, IUserBiz userBiz) : base(logger)
        {
            _config = config;
            _userBiz = userBiz;
        }

        [HttpGet("Discord", Name = "DiscordOAuth")]
        public IActionResult DiscordOAuth()
        {
            var discordConfigs = _config.GetSection("DiscordInformation");
            var baseURL = discordConfigs["OAuthURL"]!;
            var clientId = discordConfigs["ClientId"]!;
            var scopes = discordConfigs["Scopes"]!;
            var redirectUri = Url.Action("Callback", "Auth", new { version = "1" }, Request.Scheme)!;
            var state = Guid.NewGuid().ToString(); // optional for CSRF protection
            HttpContext.Session.SetString("oauth_state", state);
            var oauthUrl = $"{baseURL}&client_id={clientId}&redirect_uri={redirectUri}&scope={scopes}&state={state}";
            return Redirect(oauthUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            var expectedState = HttpContext.Session.GetString("oauth_state");
            if (state != expectedState)
                return BadRequest("Invalid state");
            var discordConfigs = _config.GetSection("DiscordInformation");
            // Exchange code for tokens
            using var client = new HttpClient();
            var tokenResponse = await client.PostAsync(discordConfigs["TokenURL"]!, new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = Url.Action("Callback", "Auth", new { version = "1" }, Request.Scheme)!,
                ["client_id"] = discordConfigs["ClientId"]!,
                ["client_secret"] = discordConfigs["ClientSecret"]!,
            }));
            var content = await tokenResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<DiscordTokenResponse>(content);

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response!.AccessToken);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("TheRemembererApp/1.0 (localhost; CSharpHttpClient)");

            var dataResponse = await client.GetAsync($"{discordConfigs["ApiURL"]}/users/@me");
            var dataContent = await dataResponse.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(dataContent);
            var root = doc.RootElement;
            var userData = new UserDto
            {
                DiscordId = ulong.Parse(root.GetProperty("id").GetString()!, System.Globalization.CultureInfo.InvariantCulture),
                DisplayName = root.GetProperty("global_name").GetString()!,
                UserName = root.GetProperty("username").GetString()!,
                AvatarHash = root.GetProperty("avatar").ToString()!,
                AccessToken = response!.AccessToken,
                RefreshToken = response!.RefreshToken,
                TokenExpiration = DateTime.UtcNow.AddSeconds(response!.ExpiresIn)
            };
            _userBiz.Upsert(userData);

            return Ok();
        }
        [HttpGet("TEST/DISCORD", Name = "DiscordData")]
        public async Task<IActionResult> GetInfo()
        {
            var discordConfigs = _config.GetSection("DiscordInformation");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "MTQyNjgxNzg1NTkyNzYxNTU0OQ.LlHps3G481jtqo8lUUaOoB530YmNAv");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("TheRemembererApp/1.0 (localhost; CSharpHttpClient)");
            var response = await client.GetAsync($"{discordConfigs["ApiURL"]}/users/@me");
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);

        }
    }
}
