using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class MovieDbContext:DbContext
    {
        public MovieDbContext():base("name=conn")
        {

        }
        public DbSet<Movie> Movies { get; set; }
    }
}