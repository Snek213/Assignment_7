using Assignment_7.Data;
using Assignment_7.Models;
using Assignment_7.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_7.Pages.Movies
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Assignment_7.Data.IMovieRepo _movieRepo;
        private readonly IWebHostEnvironment _env;

        public CreateModel(Assignment_7.Data.IMovieRepo movieRepo, IWebHostEnvironment env)
        {
            _movieRepo = movieRepo;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                Movie.PictureUri = PictureHelper.UploadNewImage(_env,
                    HttpContext.Request.Form.Files[0]);
            }

            // update the database
            _movieRepo.Add(Movie);
            _movieRepo.SaveChanges();

            // redirect to the index page where the table of all items is
            return RedirectToPage("./Index");
        }

    }

}
