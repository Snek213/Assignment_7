using Assignment_7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_7.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Assignment_7.Data.IMovieRepo _movieRepo;

        public DetailsModel(Assignment_7.Data.IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var movie = _movieRepo.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            Movie = movie;
            return Page();
        }
    }
}
