namespace OfficeAvenger.Web.Controllers.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Services;

    public static class ServiceResponseExtensions
    {
        public static void GoBabyGo(this ServiceResponse response)
        {
            if (response.HasError)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(response.Exception.ToString())
                });
            }
        }

        public static T GoBabyGo<T>(this ServiceResponse<T> response)
        {
            if (response.HasError)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(response.Exception.ToString())
                });
            }
            return response.Result;
        }
    }
}