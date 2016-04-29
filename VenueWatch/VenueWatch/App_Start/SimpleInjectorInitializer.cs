using System;
using System.Data.Entity;
using System.Net.Http.Headers;
using System.Web.Security;
using GogoKit;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VenueWatch.Models;

[assembly: WebActivator.PostApplicationStartMethod(typeof(VenueWatch.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace VenueWatch.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    public static class SimpleInjectorInitializer
    {

        private const string CLIENT_ID = "TaRJnBcw1ZvYOXENCtj5";
        private const string CLIENT_SECRET = "ixGDUqRA5coOHf3FQysjd704BPptwbk6zZreELW2aCYSmIT8XJ9ngvN1MuKV";


        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.RegisterPerWebRequest<IViagogoClient>(() =>
            {
                var client = new ViagogoClient(CLIENT_ID,
                    CLIENT_SECRET,
                    new ProductHeaderValue("VenueWatch"));
                // Its a shame to make it synchronous, but future plans will be to use another way of setting the token 
                // so that we don't have to do this.
                var token = client.OAuth2.GetClientAccessTokenAsync(new[] {"read:user"}).Result;
                client.TokenStore.SetTokenAsync(token).RunSynchronously();

                return client;
            });
            container.Register<ApplicationUserManager>();
            container.Register<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
        }
    }
}