using TheRememberer.Objects.Entities;

namespace TheRememberer.Objects.DTOs
{
    public record User_DiscordDto : DtoBase
    {
        public ulong DiscordId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string AvatarHash { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpiration { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
    }
}
