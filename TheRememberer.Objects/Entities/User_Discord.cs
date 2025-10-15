namespace TheRememberer.Objects.Entities
{
    public class User_Discord : EntityBase
    {
        public ulong DiscordId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string AvatarHash { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpiration { get; set; } = DateTime.UtcNow;

        // Add explicit foreign key to User
        public Guid UserId { get; set; }

        // Navigation property (back-reference)
        public User User { get; set; } = null!;
    }
}
