using MoviDBLibrary.DataAccess.EF.Context;
using MoviDBLibrary.DataAccess.EF.Models;
using MoviDBLibrary.DataAccess.EF.Repositories;

namespace MovieDBLibrary.Models
{
    public class MoviesViewModel
    {
        private MovieRepository _repo;
        public List<Movies> MovieList { get; set; } 
        public Movies CurrentMovie { get; set; } = null!;
        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; }

        public MoviesViewModel(MovieDbContext context)
        {
            _repo = new MovieRepository(context);
            MovieList = GetAllMovies();
            CurrentMovie = MovieList.FirstOrDefault()!;
        }
        public MoviesViewModel(MovieDbContext context, int movieId)
        {
            _repo = new MovieRepository(context);
            MovieList = GetAllMovies();
            if(movieId > 0)
            {
                CurrentMovie = GetMovie(movieId);
            }
            else
            {
                CurrentMovie = new Movies();
            }
        }

        public void SaveMovie(Movies movies)
        {
            if (movies.MovieId > 0)
            {
                _repo.Update(movies);
            }
            else
            {
                movies.MovieId = _repo.Create(movies);
            }

            MovieList = GetAllMovies();
            CurrentMovie = GetMovie(movies.MovieId);
        }

        public void RemoveMovie(int movieId)
        {
            _repo.Delete(movieId);
            MovieList = GetAllMovies();
            CurrentMovie = MovieList.FirstOrDefault()!;

        }
        public List<Movies> GetAllMovies()
        {
            return _repo.GetAllMovies();
        }
        public Movies GetMovie(int movieId)
        {
            return _repo.GetMoviesByID(movieId);
        }

    }
}
