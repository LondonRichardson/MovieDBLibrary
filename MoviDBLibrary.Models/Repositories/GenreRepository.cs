using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;

namespace MoviDBLibrary.DataAccess.EF.Repositories
{
    public class GenreRepository
    {
        private MovieDbContext _dbContext;


        public GenreRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Create(Genre genres)
        {
            _dbContext.Add(genres);
            _dbContext.SaveChanges();

            return genres.Id;
        }
        public int Update(Genre genres)
        {
            Genre existingGenre = _dbContext.Genres.Find(genres.Id)!;

            existingGenre.Genre1 = existingGenre.Genre1;
            existingGenre.Movies = existingGenre.Movies;
           
            _dbContext.SaveChanges();
            return existingGenre.Id;
        }
        public bool Delete(int Id)
        {
            Genre genres = _dbContext.Genres.Find(Id)!;
            _dbContext.Remove(genres);
            _dbContext.SaveChanges();

            return true;
        }

        public List<Genre> GetAllGenre()
        {
            List<Genre> genresList = _dbContext.Genres.ToList();

            return genresList;
        }

        public Genre GetGenreByID(int genreId)
        {
            Genre genres = _dbContext.Genres.Find(genreId);

            return genres;
        }

    }
}
