namespace TheRememberer.Objects.Entities
{
    public class Image
    {
        public int Id { get; set; }  // EF automatically treats 'Id' as primary key
        public string Image_Name { get; set; } = string.Empty;
        public string File_Name { get; set; } = string.Empty;
        public DateTime Uploaded_At { get; set; } = DateTime.UtcNow;
    }
}
