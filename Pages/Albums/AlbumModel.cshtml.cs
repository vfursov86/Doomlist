using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoomlistApp.Pages.Albums
{
    public class AlbumModel : PageModel
    {
        private readonly DoomlistContext _context;

        public AlbumModel(DoomlistContext context)
        {
            _context = context;
        }

        public Album? Album { get; set; }
        public List<Track> Tracks { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            Album = _context.Albums.FirstOrDefault(a => a.Id == id);

            if (Album == null)
            {
                return NotFound();
            }

            Tracks = _context.Tracks
                .Where(t => t.AlbumId == id)
                .OrderBy(t => t.TrackNumber)
                .ToList();

            Reviews = _context.Reviews
                .Where(r => r.AlbumId == id)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return Page();
        }

        public string GetStarRating(int? rating)
        {
            const int maxRating = 10;
            int safeRating = rating ?? 0; // Convert null to 0

            var fullStars = new string('★', safeRating); // Fully filled stars
            var emptyStars = new string('☆', maxRating - safeRating); // Outlined stars

            return fullStars + emptyStars;
        }
    }
}
