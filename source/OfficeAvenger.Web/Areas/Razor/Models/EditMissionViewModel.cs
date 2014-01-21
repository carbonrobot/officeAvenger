using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeAvenger.Domain;

namespace OfficeAvenger.Web.Areas.Razor.Models
{
    public class EditMissionViewModel
    {
        public EditMissionViewModel() { }
        public EditMissionViewModel(Mission mission)
        {
            this.Mission = mission;
        }

        public Mission Mission { get; set; }
    }
}