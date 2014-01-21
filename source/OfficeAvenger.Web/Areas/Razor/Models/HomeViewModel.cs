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
            this.Missions = new List<Mission>();
        }

        public HomeViewModel(IList<Avenger> avengers, IList<Mission> missions)
        {
            this.Avengers = avengers;
            this.Missions = missions;
        }

        public IList<Avenger> Avengers { get; set; }
        public IList<Mission> Missions { get; set; }
    }
}