using Assignment_7.Data;
using Assignment_7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_7.Pages.Movies
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Assignment_7.Data.IMovieRepo _movieRepo;

        public DeleteModel(Assignment_7.Data.IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieRepo.GetByIdAsync(id.Value);

            if (movie is not null)
            {
                Movie = movie;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieRepo.GetByIdAsync(id.Value);
            if (movie != null)
            {
                Movie = movie;
                await _movieRepo.DeleteByIdAsync(Movie.Id);
                await _movieRepo.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
