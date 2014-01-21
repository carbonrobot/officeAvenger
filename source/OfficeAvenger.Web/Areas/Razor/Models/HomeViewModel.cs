using System;
using System.Collections.Generic;
using System.Linq;
using OfficeAvenger.Domain;

namespace OfficeAvenger.Web.Areas.Razor.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.Avengers = new List<Avenger>();
        }

        public HomeViewModel(IList<Avenger> avengers)
        {
            this.Avengers = avengers;
        }

        public IList<Avenger> Avengers { get; set; }
    }
}