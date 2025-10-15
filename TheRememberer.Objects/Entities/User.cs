namespace TheRememberer.Objects.Entities
{
    public class User : EntityBase
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public User_Discord? DiscordData { get; set; }
        public ICollection<Image> UploadedImages { get; set; } = new List<Image>();
    }
}
