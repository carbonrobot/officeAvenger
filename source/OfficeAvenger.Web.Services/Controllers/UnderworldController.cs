using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Ninject;
using OfficeAvenger.Services;

namespace OfficeAvenger.Web.Services.Controllers
{
    public class UnderworldController : ApiController
    {
        [Inject]
        public DataService DataService { get; set; }
    }
}