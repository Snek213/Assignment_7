using Assignment_7.Data;
using Assignment_7.Models;
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
    public class IndexModel : PageModel
    {
        private readonly Assignment_7.Data.IMovieRepo _movieRepo;

        public IndexModel(Assignment_7.Data.IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public IList<Movie> Movie { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }


        public async Task OnGetAsync()
        {
            // <snippet_search_linqQuery>
            IEnumerable<string> genreQuery = from m in _movieRepo.GetAll()
                                        orderby m.Genre
                                        select m.Genre;
            // </snippet_search_linqQuery>

            var movies = from m in _movieRepo.GetAll()
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            // <snippet_search_selectList>
            Genres = new SelectList(genreQuery.Distinct().ToList());
            // </snippet_search_selectList>

            // The assignment said to sort movies in ascending order which sounds like ascending order top to bottom
            // but I was not sure so if it needs to be descending order it can be changed from OrderBy to OrderByDescending
            Movie = movies.OrderBy(m => m.Rating).ToList();
        }
    }
}
