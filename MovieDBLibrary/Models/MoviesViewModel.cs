using Microsoft.AspNetCore.Mvc;
using MoviDBLibrary.DataAccess.EF.Repositories;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;

namespace MovieDBLibrary.Models
{
    public class MoviesViewModel
    {
        private MovieRepository _repo;
        public List<Movie> MovieList { get; set; }
        public Movie CurrentMovie { get; set; } = null!;
        public bool IsActionSuccess { get; set; }

        public string? ActionMessage { get; set; }

        public MoviesViewModel(MovieDbContext context)
        {
            _repo = new MovieRepository(context);
            MovieList = GetMoviesWithGenres();
            CurrentMovie = MovieList.FirstOrDefault()!;
        }
        public MoviesViewModel(MovieDbContext context, int movieId)
        {
            _repo = new MovieRepository(context);
            MovieList = GetMoviesWithGenres();
            if (movieId > 0)
            {
                CurrentMovie = GetMovie(movieId);
            }
            else
            {
                CurrentMovie = new Movie();
            }
        }

        public void SaveMovie(Movie movies)
        {
            if (movies.MovieId > 0)
            {
                _repo.Update(movies);
            }
            else
            {
                movies.MovieId = _repo.Create(movies);
            }

            MovieList = GetMoviesWithGenres();
            CurrentMovie = GetMovie(movies.MovieId);
        }

        public void RemoveMovie(int movieId)
        {
            _repo.Delete(movieId);
            MovieList = GetMoviesWithGenres();
            CurrentMovie = MovieList.FirstOrDefault()!;

        }
        public List<Movie> GetMoviesWithGenres()
        {
            return _repo.GetMoviesWithGenres();
        }
       
        
        public Movie GetMovie(int movieId)
        {
            return _repo.GetMoviesByID(movieId);
        }

    }
}
