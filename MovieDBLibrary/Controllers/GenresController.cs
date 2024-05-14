using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDBLibrary.DataAccess.EF;
using MovieDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.Models;
using System.Collections.Generic;


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
        public IActionResult Index(int id,string genre1)
        {

            GenresViewModel model = new GenresViewModel(_dBContext);

            Genre genres = new(id, genre1);

            model.SaveGenre(genres);
            model.IsActionSuccess = true;
            model.ActionMessage = "Genre has been saved successfully";
            return View(model);
        }

        
        public IActionResult Update(int id)
        {
            GenresViewModel model = new GenresViewModel(_dBContext, id);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            GenresViewModel model = new GenresViewModel(_dBContext);

            if (id > 0)
            {
                model.RemoveGenre(id);
            }
            model.IsActionSuccess = true;
            model.ActionMessage = "Genre has been deleted successfully";
            return View("Index", model);

        }


    }
}
