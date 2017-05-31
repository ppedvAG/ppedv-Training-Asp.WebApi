using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Videothek.Core;

namespace Videothek.Data.Repositories
{
    interface IMovieRepository : IDisposable
    {
        Task<List<Movie>> GetAllMovies();
        Task<Movie> GetMovie(int id);

        Task<List<Movie>> GetByGenreId(int genreId);
        Task<List<Movie>> GetByGenreName(string genreName);


        Task<int> AddMovie(Movie movie);
        Task<int> UpdateMovie(Movie movie);
        Task<int> DeleteMovie(Movie movie);
    }
}
