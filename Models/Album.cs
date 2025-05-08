public class Album
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Artist { get; set; }
    public string? Genre { get; set; }
    public int? ReleaseYear { get; set; }
    public int? Rating { get; set; }
    public string? Metadata { get; set; }
    public byte[]? AlbumCover { get; set; }
}
