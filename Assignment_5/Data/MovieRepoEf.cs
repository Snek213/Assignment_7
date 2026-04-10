using Assignment_5.Data;
using Assignment_7.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Assignment_7.Data
{
    public class MovieRepoEf : IMovieRepo
    {
        private readonly Assignment_5Context _context;

        public MovieRepoEf(Assignment_5Context context)
        {
            _context = context;
        }

        public IEnumerable<Assignment_7.Models.Movie> GetAll()
        {
            return _context.Movie.OrderBy(m => m.Rating).ThenBy(m => m.Title).ToList();
        }


        public Assignment_7.Models.Movie? GetById(int id)
        {
            return _context.Movie.FirstOrDefault(m => m.Id == id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
		}   

        public void Add(Assignment_7.Models.Movie movie)
        {
            _context.Movie.Add(movie);
		}

        public object Attach(Assignment_7.Models.Movie movie)
        {
            return _context.Movie.Attach(movie);
        }
        public async Task<Assignment_7.Models.Movie> GetByIdAsync(int value)
        {
            return await _context.Movie.FirstOrDefaultAsync(m => m.Id == value);
		}
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
		}


	}
}
