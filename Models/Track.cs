public class Track
{
    public int Id { get; set; }
    public int AlbumId { get; set; } // Links track to an album
    public int TrackNumber { get; set; }
    public string Title { get; set; } = string.Empty;

    // Navigation property (optional)
    public Album Album { get; set; } 
}
