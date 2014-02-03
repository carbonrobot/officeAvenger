using System.Web.Http;
using Ninject;
using OfficeAvenger.Services;
namespace OfficeAvenger.Web.Controllers.Api
{
    public class DataApiController : ApiController
    {
        [Inject]
        public DataService DataService { get; set; }
    }
}