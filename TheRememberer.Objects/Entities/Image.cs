namespace TheRememberer.Objects.Entities
{
    public class Image : EntityBase
    {
        public string ImageName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public Guid UploaderId { get; set; }
        public User Uploader { get; set; } = null!;


        public ICollection<ImageTag> ImageTags { get; set; } = new List<ImageTag>();
    }
}
