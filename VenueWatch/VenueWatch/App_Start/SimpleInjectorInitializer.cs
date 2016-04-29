using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using GogoKit;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.WebApi;
using VenueWatch.Controllers;
using VenueWatch.Models;

[assembly: WebActivator.PostApplicationStartMethod(typeof(VenueWatch.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace VenueWatch.App_Start
{
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;

    public static class SimpleInjectorInitializer
    {

        private const string CLIENT_ID = "TaRJnBcw1ZvYOXENCtj5";
        private const string CLIENT_SECRET = "ixGDUqRA5coOHf3FQysjd704BPptwbk6zZreELW2aCYSmIT8XJ9ngvN1MuKV";


        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
           
            container.Verify(VerificationOption.VerifyOnly);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            container.RegisterPerWebRequest<IVenueWatchRepository, VenueWatchRepository>();
            container.RegisterPerWebRequest<ICurrentUserRetriever, CurrentUserRetriever>();

            container.RegisterPerWebRequest<IViagogoClient>(() =>
            {
                var client = new ViagogoClient(CLIENT_ID,
                       CLIENT_SECRET,
                       new ProductHeaderValue("VenueWatch"));
                if (!AdvancedExtensions.IsVerifying(container))
                {
                   
                    // Its a shame to make it synchronous, but future plans will be to use another way of setting the token 
                    // so that we don't have to do this.
                    var token = client.OAuth2.GetClientAccessTokenAsync(new[] {"read:user"}).Result;
                    client.TokenStore.SetTokenAsync(token);
                }
                return client;
            });
            container.RegisterPerWebRequest<ApplicationUserManager>(() => new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())));
            container.RegisterPerWebRequest<IAuthenticationManager>(() =>
                    AdvancedExtensions.IsVerifying(container)
                        ? new OwinContext(new Dictionary<string, object>()).Authentication
                        : HttpContext.Current.GetOwinContext().Authentication); 
                        }
    }
}