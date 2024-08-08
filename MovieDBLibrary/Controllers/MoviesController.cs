using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviDBLibrary.DataAccess.EF.Models;
using MoviDBLibrary.DataAccess.EF.Repositories;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.Models;
using System.Linq;



namespace MovieDBLibrary.Controllers
{
    public class MoviesController : Controller
    {
        

        private readonly MovieDbContext _dBContext;
        private MovieRepository _repo;

        public MoviesController(MovieDbContext dBContext)
        {
            _dBContext = dBContext;
            _repo = new MovieRepository(dBContext);
        }
       
        public IActionResult Index()
        {
            MoviesViewModel model = new MoviesViewModel(_dBContext);
            return View(model);
        }

        public IActionResult Action(string searchString, string searchBy)
        {
            MoviesViewModel model = new MoviesViewModel(_dBContext);

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchBy))
            {
                model.MovieList = _repo.SearchMovies(searchString, searchBy);
            }
            else
            {
                model.MovieList = _repo.GetMoviesWithGenres();
            }

            return View("Index",model);
        }

        [HttpPost]
        public IActionResult Index(int movieId, int genreId, string title, int yearReleased, string director, string leadActorActress, string cast, string grossRevenue, string maturityRating,  ICollection<UserList> userLists)
        {
            
            MoviesViewModel model = new MoviesViewModel(_dBContext);

            Movie movie = new(movieId, genreId, title, yearReleased, director, leadActorActress, cast, grossRevenue, maturityRating,  userLists);

            model.SaveMovie(movie);
            model.IsActionSuccess = true;
            model.ActionMessage = "Movie has been saved successfully";

            return View(model);
        }

        
        
        public IActionResult Update(int id)
        {
            MoviesViewModel model = new MoviesViewModel(_dBContext, id);
            return View(model);
        }
       
        public IActionResult Delete(int id)
        {
            MoviesViewModel model = new MoviesViewModel(_dBContext);

            if(id > 0)
            {
                model.RemoveMovie(id);
            }
            model.IsActionSuccess = true;
            model.ActionMessage = "Movie has been deleted successfully";
            return View("Index", model);
            
        }
    }
}
