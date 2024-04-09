using Microsoft.AspNetCore.Mvc;
using MoviDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.Models;


namespace MovieDBLibrary.Controllers
{
    public class MoviesController : Controller
    {

        private readonly MovieDbContext _dBContext;

        public MoviesController(MovieDbContext dBContext)
        {
            _dBContext = dBContext;
        }


       
        public IActionResult Index()
        {
            MoviesViewModel model = new MoviesViewModel(_dBContext);
            return View(model);
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
