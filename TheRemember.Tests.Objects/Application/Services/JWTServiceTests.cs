using Microsoft.Extensions.Configuration;
using TheRememberer.Objects.Interfaces.Services;


namespace TheRemember.Tests.Application.Services
{
    public class JWTServiceTests
    {
        private IJwtService GetJwtService()
        {
            // Build an in-memory configuration
            var inMemorySettings = new Dictionary<string, string> {
            {"JwtService:SecretKey", "BrvvUB9qUDu@PtCBHKFrp93^HpHCH^SLNR3VKcA3dJVB1"},
            {"JwtService:Issuer", "TestIssuer"},
            {"JwtService:Audience", "TestAudience"}
        };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            return new JwtService(configuration);
        }
        [Fact]
        public void CreateAccessToken_Returns_NonEmptyString()
        {
            var service = GetJwtService();
            var userId = Guid.NewGuid();

            var token = service.CreateAccessToken(userId);

            Assert.False(string.IsNullOrWhiteSpace(token));
        }

        [Fact]
        public void CreateRefreshToken_Returns_NonEmptyString()
        {
            var service = GetJwtService();

            var token = service.CreateRefreshToken();

            Assert.False(string.IsNullOrWhiteSpace(token));
        }
    }
}