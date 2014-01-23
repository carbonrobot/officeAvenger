using System;
using System.Linq;
using System.Web.Mvc;
using OfficeAvenger.Web.Security;
using OfficeAvenger.Web.Security.Handlers;

namespace OfficeAvenger.Web
{
    public static class SecurityConfig
    {
        public static void Register()
        {
            // TODO:    this gets a bit weird, b/c FormsAuth has some methods that are reqd to be static
            //          so matrix actually gets set internally to a static, which is terrible for threading
            //          need to find a way around this, as we should actually have a new instance for each
            //          call to the api in order to have a nice clean unit of work

            // This is the ideal pattern we want to deploy with, i think
            //
            // FormsAuthentication.ConfigureWith<LazySecurityMatrix>();
            // TokenAuthentication.ConfigureWith<LazySecurityMatrix>();
            // Shield.Register<FormsAuthentication>();
            // Shield.Register<TokenAuthentication>();
            //
            // or possibly something like this....
            //
            // Shield.Register<FormsAuthentication>().Using<LazySecurityMatrix>();
            // Shield.Register<TokenAuthentication>().Using<LazySecurityMatrix>();
            //

            var matrix = new LazySecurityMatrix();
            Shield.Register(new FormsAuthentication(matrix));
            Shield.Register(new TokenAuthentication(matrix));
        }
    }
}