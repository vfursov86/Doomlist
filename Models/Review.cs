public class Review
{
    public int Id { get; set; }
    public int AlbumId { get; set; }  // Links review to an album
    public string ReviewText { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
