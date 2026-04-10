using Microsoft.EntityFrameworkCore;
namespace Assignment_5.Data
{
    public class MovieRepoList
    {
        private readonly List<Assignment_7.Models.Movie> _movies;

        public MovieRepoList()
        {
            _movies = new List<Assignment_7.Models.Movie>
        {
            new Assignment_7.Models.Movie { Id = 1, Title = "When Harry Met Sally", ReleaseDate = DateTime.Parse("1989-2-12"), Genre = "Romantic Comedy", Price = 7.99M },
            new Assignment_7.Models.Movie { Id = 2, Title = "Ghostbusters ", ReleaseDate = DateTime.Parse("1984-3-13"), Genre = "Comedy", Price = 8.99M },
            new Assignment_7.Models.Movie { Id = 3, Title = "Ghostbusters 2", ReleaseDate = DateTime.Parse("1986-2-23"), Genre = "Comedy", Price = 9.99M },
            new Assignment_7.Models.Movie { Id = 4, Title = "Rio Bravo", ReleaseDate = DateTime.Parse("1959-4-15"), Genre = "Western", Price = 3.99M }
        };
        }

        public IEnumerable<Assignment_7.Models.Movie> GetAll()
        {
            return _movies.OrderBy(m => m.Rating).ThenBy(m => m.Title).ToList();
        }

        public async Task<IEnumerable<Assignment_7.Models.Movie>> GetAllAsync()
        {
            return await Task.FromResult(_movies.OrderBy(m => m.Rating)
                .ThenBy(m => m.Title).ToList());
        }

        public Assignment_7.Models.Movie? GetById(int id)
        {
            return _movies.FirstOrDefault(m => m.Id == id);
        }

        public async Task<Assignment_7.Models.Movie?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_movies.FirstOrDefault(m => m.Id == id));
        }
    }
}
