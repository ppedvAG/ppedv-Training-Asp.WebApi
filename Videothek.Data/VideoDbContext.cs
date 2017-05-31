using System.Data.Entity;
using Videothek.Core;

namespace Videothek.Data
{
    public class VideoDbContext : DbContext
    {
        public VideoDbContext() : base("VideoDb")
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
