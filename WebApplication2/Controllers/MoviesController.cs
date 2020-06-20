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
        [HttpPost]
        public ActionResult Save(Movie Movie)
        {
            if (Movie.Id == 0)
            {
                Movie.DateAdded = DateTime.Now;
                _context.Movies.Add(Movie);
            }
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == Movie.Id);
                movieInDb.Name = Movie.Name;
                movieInDb.GenreId = Movie.GenreId;
                movieInDb.DateAdded = DateTime.Now;
                movieInDb.ReleaseDate = Movie.ReleaseDate;
                movieInDb.NumberInStock = Movie.NumberInStock;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "Movies");
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
                Movie = movie,
                Genres = _context.Genres.ToList()
            };
            return View("New", viewmodel);
        }
    }
}