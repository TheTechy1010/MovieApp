using MovieApp.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieApp.Models
{
    public class MovieHelper : IMovieHelper
    {
        private MovieDbContext _db;
        public MovieHelper()
        {
            _db = new MovieDbContext();
        }

        public void AddMovie(Movie movie)
        {
            
        }

        public void DeleteMovie()
        {
            throw new NotImplementedException();
        }

        public void EditMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Movie> GetAllMovies(int? page)
        {
            var all = _db.Movies.OrderByDescending(m => m.Id).ToPagedList(page ?? 1, 8);
            return all;
        }

        public IPagedList<Movie> GetFilteredMovies(string filter, string searchTerm, int? page)
        {
            
                var filteredMovies = _db.Movies.AsQueryable();
                string search = "";
                if (!string.IsNullOrWhiteSpace(filter))
                {
                var value = (Genre)(Convert.ToInt32(filter));
                    filteredMovies = _db.Movies.Where(m => m.Genre == value);
                    search = value.ToString();
                }

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    filteredMovies = filteredMovies.Where(m => m.Name.ToLower().Contains(searchTerm.ToLower()));
                    search += (" / " + searchTerm);
                }


                filteredMovies = filteredMovies.OrderByDescending(m => m.Id);

            return filteredMovies.ToPagedList(page ?? 1, 8);
        }

        public Movie GetMovie(int id)
        {
            var movie = _db.Movies.Find(id);
            return movie;
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
                var path = "~/Content/noPoster.png";
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