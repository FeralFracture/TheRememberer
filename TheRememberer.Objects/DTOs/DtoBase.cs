namespace TheRememberer.Objects.DTOs
{
    public abstract record DtoBase
    {
        public Guid? DbId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
