using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeAvenger.Domain;

namespace OfficeAvenger.Web.Areas.Razor.Models
{
    public class EditTeamViewModel
    {
        public EditTeamViewModel() { }
        public EditTeamViewModel(Avenger entity)
        {
            this.Avenger = entity;
        }

        public Avenger Avenger { get; set; }
    }
}