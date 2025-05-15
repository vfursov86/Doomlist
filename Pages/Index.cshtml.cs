using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoomlistApp.Pages
{
    public class AlbumsModel : PageModel
    {
        private readonly DoomlistContext _context;

        public AlbumsModel(DoomlistContext context)
        {
            _context = context;
        }

        public List<Album> Albums { get; set; } = new();
        public Album? SelectedAlbum { get; set; } // Holds specific album details when viewing a single album
        public List<Track> Tracks { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<SelectListItem> Genres { get; set; } = new();
        public List<SelectListItem> Artists { get; set; } = new();

        public string? SelectedGenre { get; set; }
        public int? SelectedYear { get; set; }
        public string? SelectedArtist { get; set; }
        public int? SelectedRating { get; set; }

        public void OnGet(int? albumId, string? selectedGenre, int? selectedYear, string? selectedArtist, int? selectedRating)
        {
            // Fetch distinct genres & artists for filtering dropdowns
            Genres = _context.Genres
                .OrderBy(g => g.GenreName)
                .Select(g => new SelectListItem { Value = g.GenreName, Text = g.GenreName })
                .ToList();

            Artists = _context.Albums
                .Select(a => a.Artist)
                .Distinct()
                .OrderBy(a => a)
                .Select(a => new SelectListItem { Value = a, Text = a })
                .ToList();

            SelectedGenre = selectedGenre;
            SelectedYear = selectedYear;
            SelectedArtist = selectedArtist;
            SelectedRating = selectedRating;

            var query = _context.Albums.AsQueryable();

            if (!string.IsNullOrEmpty(SelectedGenre))
                query = query.Where(a => a.Genre == SelectedGenre);

            if (SelectedYear.HasValue)
                query = query.Where(a => a.ReleaseYear == SelectedYear.Value);

            if (!string.IsNullOrEmpty(SelectedArtist))
                query = query.Where(a => a.Artist.Contains(SelectedArtist));

            if (SelectedRating.HasValue)
                query = query.Where(a => a.Rating >= SelectedRating.Value);

            if (albumId.HasValue)
            {
                // Fetch only the selected album instead of loading all albums
                SelectedAlbum = _context.Albums.FirstOrDefault(a => a.Id == albumId.Value);

                if (SelectedAlbum != null)
                {
                    Tracks = _context.Tracks
                        .Where(t => t.AlbumId == albumId.Value)
                        .OrderBy(t => t.TrackNumber)
                        .ToList();

                    Reviews = _context.Reviews
                        .Where(r => r.AlbumId == albumId.Value)
                        .OrderByDescending(r => r.CreatedAt)
                        .ToList();
                }
            }
            else
            {
                Albums = query.ToList();
            }
        }
    }
}
