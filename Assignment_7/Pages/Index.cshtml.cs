using Assignment_7.Data;
using Assignment_7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_7.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Assignment_7.Data.IMovieRepo _movieRepo;

        public IndexModel(Assignment_7.Data.IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }
        public IList<Movie> Movies { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Movies = _movieRepo.GetAll().ToList();

        }
    }
}
