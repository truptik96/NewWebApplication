using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class MoviesController : Controller
    {
        public ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool Disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);
            return View(movie);
        }
        public ActionResult Save(Movie Movie)
        {
            if (Movie.Id == 0)
                _context.Movies.Add(Movie);
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == Movie.Id);
                movieInDb.Name = Movie.Name;
                movieInDb.GenreId = Movie.GenreId;
                movieInDb.DateAdded = Movie.DateAdded;
                movieInDb.ReleaseDate = Movie.ReleaseDate;
                movieInDb.NumberInStock = Movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        public ActionResult New()
        {
            var genre = _context.Genres.ToList();
            var viewmodel = new MovieFormViewModel()
            {
                Genres = genre
            };
            return View(viewmodel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewmodel = new MovieFormViewModel()
            {
                Movies = movie,
                Genres = _context.Genres.ToList()
            };
            return View("New", viewmodel);
        }
    }
}