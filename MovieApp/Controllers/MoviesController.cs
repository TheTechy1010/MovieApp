using MovieApp.Interfaces;
using MovieApp.Models;
using MovieApp.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDbContext _db;
        private IMovieHelper _helper;
        public MoviesController()
        {
            _db = new MovieDbContext();
            _helper = new MovieHelper();
        }
        // GET: Movies
        public ActionResult AllMovies(string filter, string searchTerm, int? page)
        {
            if (!string.IsNullOrWhiteSpace(filter) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                var filtered = _helper.GetFilteredMovies(filter, searchTerm, page);
                return View(filtered);
            }
            var all = _helper.GetAllMovies(page);
            return View(all);

        }
        public ActionResult AddMovie()
        {
            var viewModel = new MovieFormViewModel();
            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(MovieFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Name = viewModel.Name,
                    Genre = viewModel.Genre,
                    Price = (double)viewModel.Price,
                    ReleaseDate = (DateTime)viewModel.ReleaseDate
                };
                movie.Poster = CheckWallpaper(viewModel.Poster);
                _db.Movies.Add(movie);
                _db.SaveChanges();
                return RedirectToAction("AllMovies", new { page = 1 });
            }

            viewModel.getGenList();
            return View("MovieForm", viewModel);
        }
        public ActionResult EditMovie(int id)
        {
            var movie = _helper.GetMovie(id);
            if (movie == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var viewModel = new MovieFormViewModel(movie.Id)
            {

                Genre = movie.Genre,
                Name = movie.Name,
                Price = movie.Price,
                ReleaseDate = movie.ReleaseDate,
                ImageString = Convert.ToBase64String(movie.Poster),
                Availability = movie.Availability

            };

            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMovie(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("MovieForm", viewModel);
            }
            var movie = _db.Movies.Find(viewModel.Id);
            if (movie == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            movie.Name = viewModel.Name;
            movie.Price = (double)viewModel.Price;
            movie.ReleaseDate = (DateTime)viewModel.ReleaseDate;
            movie.Genre = viewModel.Genre;
            movie.Availability = viewModel.Availability;
            if (viewModel.Poster != null)
            {
                movie.Poster = ConvertToByte(viewModel.Poster);
            }
            _db.SaveChanges();
            return RedirectToAction("AllMovies", new { page = 1 });
        }

        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            _db.Movies.Remove(movie);
            _db.SaveChanges();
            return RedirectToAction("AllMovies");
        }

        public ActionResult Details(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null)
                return HttpNotFound("Movie Not Found");
            return View(movie);
        }
        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }
        public byte[] CheckWallpaper(HttpPostedFileBase file)
        {
            if (file == null)
            {
                var path = Server.MapPath("~/Content/noPoster.png");
                var defaultImage = Image.FromFile(path);
                using (MemoryStream mStream = new MemoryStream())
                {
                    defaultImage.Save(mStream, defaultImage.RawFormat);
                    return mStream.ToArray();
                }
            }
            else
            {
                return ConvertToByte(file);
            }
        }
    }
}