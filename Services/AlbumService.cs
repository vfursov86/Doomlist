public class AlbumService
{
    private readonly DoomlistContext _context;

    public AlbumService(DoomlistContext context)
    {
        _context = context;
    }

    public List<Album> GetFilteredAlbums(string genre, int? year)
    {
        var query = _context.Albums.AsQueryable();

        if (!string.IsNullOrEmpty(genre))
            query = query.Where(a => a.Genre == genre);

        if (year.HasValue)
            query = query.Where(a => a.ReleaseYear == year.Value);

        return query.ToList();
    }
}
