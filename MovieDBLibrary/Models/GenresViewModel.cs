using Microsoft.IdentityModel.Tokens;
using MoviDBLibrary.DataAccess.EF.Repositories;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;
using System.Collections.Immutable;

namespace MovieDBLibrary.Models
{
    public class GenresViewModel
    {
        private GenreRepository _repo;
        public List<Genre> GenreList { get; set; }
        
        public Movie CurrentMovie { get; set; }
        public Genre CurrentGenre { get; set; } 
        public bool IsActionSuccess { get; set; }

        public string? ActionMessage { get; set; }


        
        public GenresViewModel(MovieDbContext context)
        {
            _repo = new GenreRepository(context);
            GenreList = GetAllGenres();
            SetCurrentGenreAndMovies();
               
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
        private void SetCurrentGenreAndMovies()
        {
            if (GenreList != null && GenreList.Count > 0)
            {
                CurrentGenre = GenreList[0];
            }
            else
            {
                CurrentGenre = null;
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
        public void RemoveGenre(int genreId)
        {
            _repo.Delete(genreId);
            GenreList = GetAllGenres();
            CurrentGenre = GenreList.FirstOrDefault()!;

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
