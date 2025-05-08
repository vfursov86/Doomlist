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
        public List<SelectListItem> Genres { get; set; } = new(); // Restore Genres list

        public string? SelectedGenre { get; set; }
        public int? SelectedYear { get; set; }
        public string? SelectedArtist { get; set; } // New filter for Artist
        public int? SelectedRating { get; set; } // New filter for Rating

        public void OnGet(string? selectedGenre, int? selectedYear, string? selectedArtist, int? selectedRating)
        {
            SelectedGenre = selectedGenre;
            SelectedYear = selectedYear;
            SelectedArtist = selectedArtist;
            SelectedRating = selectedRating;

            Genres = _context.Genres
                .OrderBy(g => g.GenreName)
                .Select(g => new SelectListItem { Value = g.GenreName, Text = g.GenreName })
                .ToList();

            var query = _context.Albums.AsQueryable();

            if (!string.IsNullOrEmpty(SelectedGenre))
                query = query.Where(a => a.Genre == SelectedGenre);

            if (SelectedYear.HasValue)
                query = query.Where(a => a.ReleaseYear == SelectedYear.Value);

            if (!string.IsNullOrEmpty(SelectedArtist))
                query = query.Where(a => a.Artist.Contains(SelectedArtist)); // Allow partial matches

            if (SelectedRating.HasValue)
                query = query.Where(a => a.Rating >= SelectedRating.Value); // Filter by minimum rating

            Albums = query.ToList();
        }
    }
}
