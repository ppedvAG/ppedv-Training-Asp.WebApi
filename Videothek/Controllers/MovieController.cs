using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Videothek.Core;
using Videothek.Data.Repositories;

namespace Videothek.Controllers
{
    /// <summary>
    /// Managing all Entries of Type Movie. All Crud-Methods are available.
    /// GET Methods are public and the rest required authorization
    /// </summary>
    [Authorize]
   // [EnableCors(orgins: "*", headers: "*", methods: "*")]
    public class MovieController : ApiController
    {
        private readonly MovieRepository _movie;

        public MovieController()
        {
            _movie = new MovieRepository();
        }
        protected override void Dispose(bool disposing)
        {
            _movie.Dispose();
        }

        /// <summary>
        /// Give you a list of all Entries in the Database. Odata Querying is enabled.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        //[ResponseType(typeof(List<Movie>))]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy, MaxOrderByNodeCount = 2000, MaxNodeCount = 2000)]
        public async Task<IHttpActionResult> Get()
        {
            List<Movie> movies = await _movie.GetAllMovies();
            return Ok(movies).Cached(Cacheability.Public, maxAge: TimeSpan.FromSeconds(20));
        }

        /// <summary>
        /// Gives you the required Entrie. The search is based on the Movie Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Singe Movie</returns>
        [AllowAnonymous]
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> Get(int id)
        {
            Movie movie = await _movie.GetMovie(id);

            if (movie == null)
                return NotFound();
            else
                return Ok(movie);
        }


        [AllowAnonymous]
        [ResponseType(typeof(List<Movie>))]
        [Route("api/genre/{id:int}/movie", Name = "ByGenreId")]
        public async Task<IHttpActionResult> GetByGenreId(int id)
        {
            List<Movie> movies = await _movie.GetByGenreId(id);
            return Ok(movies);
        }

        [AllowAnonymous]
        [ResponseType(typeof(List<Movie>))]
        [Route("api/genre/{name:alpha}/movie", Name = "ByGenreName")]
        public async Task<IHttpActionResult> GetByGenreName(string name)
        {
            List<Movie> movies = await _movie.GetByGenreName(name);
            return Ok(movies);
        }



        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> Post([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _movie.AddMovie(movie);
            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        public async Task<IHttpActionResult> Put([FromBody]Movie movie)
        {
            await _movie.UpdateMovie(movie);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Admin Role2")] //UND-Verknüpfung -> User in beiden Rollen
        //[Authorize(Roles = "Admin, Role2")] //ODER-Verknüpfung -> User einer der beiden Rollen
        public async Task<IHttpActionResult> Delete(int id)
        {
            Movie movie = await _movie.GetMovie(id);

            if (movie == null)
                return NotFound();

            await _movie.DeleteMovie(movie);
            return Ok();
        }
    }
}
