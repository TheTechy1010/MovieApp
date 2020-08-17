using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieApp.ViewModels
{
    public class MovieFormViewModel
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        public double? Price { get; set; }

        public HttpPostedFileBase Poster { get; set; }

        public string Action { get; set; }
        public string Heading { get; set; }
        public IEnumerable<SelectListItem> GenList { get { return getGenList(); } set { } }
        public IEnumerable<SelectListItem> getGenList()
        {
            var list = new List<SelectListItem>()
            {

                new SelectListItem{ Text="Action",Value="0"},
                new SelectListItem{ Text="SciFi",Value="1"},
                new SelectListItem{ Text="Comedy",Value="2"},
                new SelectListItem{ Text="Drama",Value="3"},
                new SelectListItem{ Text="Thriller",Value="4"}
            };
            return list;
        }
        public IEnumerable<SelectListItem> AvailabilityList { get { return getAvailability(); } set { } }
        public IEnumerable<SelectListItem> getAvailability()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem{ Text="--Select--",Value="",Selected=true},
                new SelectListItem{ Text="Available",Value="0"},
                new SelectListItem{ Text="Not Available",Value="1"},
            };
            return list;
        }
        public string ImageString { get; set; }
        public Availability Availability { get; set; }

        public MovieFormViewModel()
        {
            Action = "AddMovie";
            Heading = "Add New Movie";
        }

        public MovieFormViewModel(int id)
        {
            this.Id = id;

            Action = "EditMovie";
            Heading = "Edit the Movie";

        }
    }
}