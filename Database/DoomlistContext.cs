using Microsoft.EntityFrameworkCore;

public class DoomlistContext : DbContext
{
    public DoomlistContext(DbContextOptions<DoomlistContext> options) : base(options) { }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Review> Reviews { get; set; }

}