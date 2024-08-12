using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;


namespace MoviDBLibrary.DataAccess.EF.Repositories
{
    public class MovieRepository
    {
        private MovieDbContext _dbContext;


        public MovieRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public int Create(Movie movie)
        {
            _dbContext.Add(movie);
            _dbContext.SaveChanges();

            return movie.MovieId;
        }
        public int Update(Movie movie)
        {
            Movie existingMovie = _dbContext.Movies.Find(movie.MovieId)!;

            existingMovie.Title = movie.Title;
            existingMovie.GenreId = movie.GenreId;
            existingMovie.Genre = movie.Genre;
            existingMovie.YearReleased = movie.YearReleased;
            existingMovie.Director = movie.Director;
            existingMovie.LeadActorActress = movie.LeadActorActress;
            existingMovie.Cast = movie.Cast;
            existingMovie.GrossRevenue = movie.GrossRevenue;
            existingMovie.MaturityRating = movie.MaturityRating;
            existingMovie.UserLists = movie.UserLists;

            _dbContext.SaveChanges();
            return existingMovie.MovieId;
        }
        public bool Delete(int movieId)
        {
            Movie movie = _dbContext.Movies.Find(movieId)!;
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();

            return true;
        }
        public List<Movie> SearchMovies(string searchTerm, string searchBy)
        {
            List<Movie> search = new List<Movie>();

            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(searchBy))
            {
                switch (searchBy.ToLower())
                {
                    case "genre":
                        // Load movies and genres into memory first
                        search = _dbContext.Movies.Include(m => m.Genre)
                                                  .AsEnumerable()
                                                  .Where(m => m.Genre.Genre1.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "actor":
                        // Load movies into memory first
                        search = _dbContext.Movies
                                           .AsEnumerable()
                                           .Where(m => !string.IsNullOrEmpty(m.LeadActorActress) && m.LeadActorActress.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                           .ToList();
                        break;
                    case "director":
                        // Load movies into memory first
                        search = _dbContext.Movies
                                           .AsEnumerable()
                                           .Where(m => !string.IsNullOrEmpty(m.Director) && m.Director.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                           .ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // If no search term, return all movies with genres included
                search = _dbContext.Movies.Include(m => m.Genre).ToList();
            }

            return search;
        }
        /*
        public List<Movie> SearchMovies(string searchTerm, string searchBy)
        {
            
            List<Movie> search = new List<Movie>();
            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(searchBy))
            {
                switch (searchBy.ToLower())
                {
                    case "genre":
                        search = _dbContext.Movies.Include(m=> m.Genre).ToList().Where(m => m.Genre.Genre1.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "actor":
                        search = _dbContext.Movies.Where(m => m.LeadActorActress != null && m.LeadActorActress.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "director":
                        search = _dbContext.Movies.Where(m => m.Director != null && m.Director.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                search = _dbContext.Movies.Include(m => m.Genre).ToList();
            }

            return search;
        }
        */
        public List<Movie> GetMoviesWithGenres()
        {
            using (var context = new MovieDbContext())
            {
                var moviesWithGenres = context.Movies.Include(m => m.Genre).ToList();
                return moviesWithGenres;
            }
        }
        
        public Movie GetMoviesByID(int movieId)
        {
            Movie movie = _dbContext.Movies.Find(movieId);

            return movie;
        }
    }
}
