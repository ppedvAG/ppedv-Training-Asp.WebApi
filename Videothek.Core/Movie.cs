using System.ComponentModel.DataAnnotations; //Entity Framework

namespace Videothek.Core
{

    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
