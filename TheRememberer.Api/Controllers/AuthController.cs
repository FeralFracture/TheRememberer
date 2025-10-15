using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces;
using TheRememberer.Objects.Interfaces.Services;

namespace TheRememberer.Api.Controllers
{
    [ApiVersion("1.0")]
    public class AuthController : DecoratedControllerBase<AuthController>
    {
        private readonly IConfiguration _config;
        private readonly IUserBiz _userBiz;
        private readonly IUser_DiscordBiz _discordBiz;
        private readonly IJwtService _jwtService;
        public AuthController(ILogger<AuthController> logger, IConfiguration config, IUserBiz userBiz, IUser_DiscordBiz discordBiz, IJwtService jwtService) : base(logger)
        {
            _config = config;
            _userBiz = userBiz;
            _discordBiz = discordBiz;
            _jwtService = jwtService;
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
            HttpContext.Session.Remove("oauth_state");


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
            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
            if (!tokenResponse.IsSuccessStatusCode)
                return BadRequest("Failed to exchange code for token");

            var tokenParsed = JsonSerializer.Deserialize<DiscordTokenResponse>(tokenContent);

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenParsed!.AccessToken);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("TheRemembererApp/1.0 (localhost; CSharpHttpClient)");

            var dataResponse = await client.GetAsync($"{discordConfigs["ApiURL"]}/users/@me");
            var dataContent = await dataResponse.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(dataContent);
            var dataJson = doc.RootElement;

            var userId = _userBiz.Upsert(new UserDto());
            var user = _userBiz.Get(userId!.Value);

            var accessToken = _jwtService.CreateAccessToken(user!.DbId!.Value);
            var refreshToken = _jwtService.CreateRefreshToken();

            user.AccessToken = accessToken;
            user.RefreshToken = refreshToken;
            user.TokenExpiration = DateTime.UtcNow.AddMinutes(15);
            user.UpdatedAt = DateTime.UtcNow;


            var discordData = new User_DiscordDto
            {
                AccessToken = tokenParsed!.AccessToken,
                RefreshToken = tokenParsed!.RefreshToken,
                TokenExpiration = DateTime.UtcNow.AddSeconds(tokenParsed!.ExpiresIn),
                AvatarHash = dataJson.GetProperty("avatar").ToString()!,
                DiscordId = ulong.Parse(dataJson.GetProperty("id").GetString()!, System.Globalization.CultureInfo.InvariantCulture),
                DisplayName = dataJson.GetProperty("global_name").GetString()!,
                UserName = dataJson.GetProperty("username").GetString()!,
                UserId = userId!.Value,
            };

            discordData.DbId = _discordBiz.Upsert(discordData);

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
