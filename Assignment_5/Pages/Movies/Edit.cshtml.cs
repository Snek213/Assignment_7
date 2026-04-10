using Assignment_7.Data;
using Assignment_7.Models;
using Assignment_7.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_7.Pages.Movies
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly Assignment_7.Data.IMovieRepo _movieRepo;
        private readonly IWebHostEnvironment _env;

        public EditModel(Assignment_7.Data.IMovieRepo movieRepo, IWebHostEnvironment env)
        {
            _movieRepo = movieRepo;
            _env = env;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie =  await _movieRepo.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];
                if (file.Length > 0)
                {
                    // Upload the new image and update the PictureUri
                    Movie.PictureUri = PictureHelper.UploadNewImage(_env, file);
                }
            }

            ((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry)_movieRepo.Attach(Movie)).State = EntityState.Modified;

            try
            {
                await _movieRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _movieRepo.GetAll().Any(e => e.Id == id);
        }
    }
}
