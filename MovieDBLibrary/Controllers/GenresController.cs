using Microsoft.AspNetCore.Mvc;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.Models;


namespace MovieDBLibrary.Controllers
{
    public class GenresController : Controller
    {

        private readonly MovieDbContext _dBContext;

        public GenresController(MovieDbContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IActionResult Index()
        {
            GenresViewModel model = new GenresViewModel(_dBContext);
            return View(model);
        }

        
        [HttpPost]
        public IActionResult Index(int id,string genre1, ICollection<Movie> movies)
        {
            GenresViewModel model = new GenresViewModel(_dBContext);

            Genre genres = new(id,genre1, movies);

            model.SaveGenres(genres);
            model.IsActionSuccess = true;
            model.ActionMessage = "Genre has been saved successfully";
            return View(model);
        }

       
        public IActionResult Update(int id)
        {
            GenresViewModel model = new GenresViewModel(_dBContext, id);
            return View(model);
        }

        
        
        
    }
}
