using MoviDBLibrary.DataAccess.EF.Repositories;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;

namespace MovieDBLibrary.Models
{
    public class GenresViewModel
    {
        private GenreRepository _repo;
        public List<Genre> GenreList { get; set; }
        public Genre CurrentGenre { get; set; } 
        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; }

        public GenresViewModel(MovieDbContext context)
        {
            _repo = new GenreRepository(context);
            GenreList = GetAllGenres();
            CurrentGenre = GenreList.FirstOrDefault()!;
        }
        public GenresViewModel(MovieDbContext context, int genreId)
        {
            _repo = new GenreRepository(context);
            GenreList = GetAllGenres();
            if (genreId > 0)
            {
                CurrentGenre = GetGenre(genreId);
            }
            else
            {
                CurrentGenre = new Genre();
            }
        }

        public void SaveGenre(Genre genres)
        {
            if (genres.Id > 0)
            {
                _repo.Update(genres);
            }
            else
            {
                genres.Id = _repo.Create(genres)!;
            }

            GenreList = GetAllGenres();
            CurrentGenre = GetGenre(genres.Id);
        }

        
        public List<Genre> GetAllGenres()
        {
            return _repo.GetAllGenre();
        }
        public Genre GetGenre(int genreId)
        {
            return _repo.GetGenreByID(genreId);
        }

        internal void SaveGenres(Genre genres)
        {
            throw new NotImplementedException();
        }
    }


}
