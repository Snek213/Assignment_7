using Assignment_7.Models;

namespace Assignment_7.Data
{
    public interface IMovieRepo
    {
        Task DeleteByIdAsync(int id);
        IEnumerable<Movie> GetAll();
        Movie? GetById(int id);
        void SaveChanges();
        void Add(Movie movie);
        object Attach(Movie movie);
        Task<Movie> GetByIdAsync(int value);
        Task SaveChangesAsync();
        Task Update(Movie movie);

    }
}