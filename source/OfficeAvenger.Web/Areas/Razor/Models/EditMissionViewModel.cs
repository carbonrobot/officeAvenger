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
        public EditMissionViewModel(Mission mission) : this(mission, null)
        {
        }
        public EditMissionViewModel(Mission mission, IList<Avenger> avengers)
        {
            this.Mission = mission;

            if(avengers != null)
                this.ReadyTeam = avengers.Except(mission.Team).ToList();
        }

        public Mission Mission { get; set; }
        public IList<Avenger> ReadyTeam { get; set; }
    }
}