using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Videothek.Core;

namespace Videothek.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly VideoDbContext context;

        public MovieRepository()
        {
            context = new VideoDbContext();
        }
        public void Dispose()
        {
            context.Dispose();
        }



        public Task<int> AddMovie(Movie movie)
        {
            context.Movies.Add(movie);
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteMovie(Movie movie)
        {
            context.Movies.Remove(movie);
            return context.SaveChangesAsync();
        }

        public Task<List<Movie>> GetAllMovies()
        {
            return context.Movies.Include(m => m.Genre).ToListAsync();
        }

        public Task<Movie> GetMovie(int id)
        {
            return context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }

        public Task<int> UpdateMovie(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }


        public Task<List<Movie>> GetByGenreId(int genreId)
        {
            return context.Movies.Include(m => m.Genre).Where(m => m.GenreId == genreId).ToListAsync();
        }

        public Task<List<Movie>> GetByGenreName(string genreName)
        {
            return context.Movies.Include(m => m.Genre).Where(m => m.Genre.Name == genreName).ToListAsync();
        }
    }
}
