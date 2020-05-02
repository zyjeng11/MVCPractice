using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {
            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult New()
        {
            var genre = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genre
            };
            
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var genre = _context.Genre.ToList();
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            var viewModel = new MovieFormViewModel
            {
                Genres = genre,
                Movies = movie
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(MovieFormViewModel model)
        {
            if(model.Movies.Id == 0)
                _context.Movies.Add(model.Movies);
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == model.Movies.Id);
                movieInDb.Name = model.Movies.Name;
                movieInDb.GenreId = model.Movies.GenreId;
                movieInDb.ReleaseDate = model.Movies.ReleaseDate;
                movieInDb.StockNumber = model.Movies.StockNumber;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }


        [Route("movies/released/{year}/{month:regex(\\{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}