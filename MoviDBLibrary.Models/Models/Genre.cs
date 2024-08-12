using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDBLibrary.DataAccess.EF.Models
{
    public class Genre
    {
        public int Id { get; set; }       
        public string Genre1 { get; set; } = null!;
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

        public Genre(int id, string genre1)
        {
            Id = id;         
            Genre1 = genre1;
           
        }

        public Genre()
        {
            
        }
    }
}