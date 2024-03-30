using Microsoft.AspNetCore.Mvc;
using MoviDBLibrary.DataAccess.EF.Context;
using MoviDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.Models;
using System.Collections.Generic;
using System.IO;


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
            MoviesViewModel model = new MoviesViewModel(_dBContext);
            return View(model);
        }

        
        [HttpPost]
        public IActionResult Index(int movieId, string title, int yearReleased, string director, string leadActorActress, string cast, string grossRevenue, string maturityRating, string imageUrl, ICollection<MoviesGenre> moviesGenres, ICollection<UserList> userLists)
        {
            MoviesViewModel model = new MoviesViewModel(_dBContext);

            Movies movie = new(movieId, title, yearReleased, director, leadActorActress, cast, grossRevenue, maturityRating, imageUrl, moviesGenres, userLists);

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
            model.ActionMessage = "Movie has been dleted successfully";
            return View("Index", model);
            
        }
    }
}
