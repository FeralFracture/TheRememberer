namespace TheRememberer.Objects.Interfaces.Services
{
    public interface IJwtService
    {
        public string CreateAccessToken(Guid userId);
        public string CreateRefreshToken();
    }
}