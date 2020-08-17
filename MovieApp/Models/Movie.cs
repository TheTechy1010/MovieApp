using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3),MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        public double Price { get; set; }

        public byte[] Poster { get; set; }

        public Availability Availability { get; set; }
    }
    public enum Genre
    {
        Action=0,SciFi,Comedy,Drama,Thriller
    }
    public enum Availability
    {
        Available=0,NotAvailable
    }
}