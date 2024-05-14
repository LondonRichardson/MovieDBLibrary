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
        public List<Movie> MoviesList { get; set; }
        public Movie CurrentMovie { get; set; }
        public Genre CurrentGenre { get; set; } 
        public bool IsActionSuccess { get; set; }

        public string? ActionMessage { get; set; }


        /*
         * Need a function to loop through your genres
         * For each genre, get a list of movies for that genre
         * Assign that list of movies to a List<Movie> on your Genre model
         * Use the Genre.MovieList on your view and loop through that list
         */
        public GenresViewModel(MovieDbContext context)
        {
            _repo = new GenreRepository(context);
            GenreList = GetAllGenres();
            CurrentGenre = GenreList.FirstOrDefault();
            MoviesList = GetMoviesByGenreId(CurrentGenre.Id);
           

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

        public List<Movie> GetMoviesByGenreId(int genreid)
        {
            return _repo.GetMoviesByGenreId(genreid).ToList();
        }
      
        internal void SaveGenres(Genre genres)
        {
            throw new NotImplementedException();
        }
    }


}
