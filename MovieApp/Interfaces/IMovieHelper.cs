using MovieApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieApp.Interfaces
{
    public interface IMovieHelper
    {
        IPagedList<Movie> GetAllMovies(int? page);
        IPagedList<Movie> GetFilteredMovies(string filter, string searchTerm, int? page);
        Movie GetMovie(int id);
        void AddMovie(Movie movie);
        void EditMovie(Movie movie);
        void DeleteMovie();
    }
}