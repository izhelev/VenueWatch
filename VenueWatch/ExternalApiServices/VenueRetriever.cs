using System;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using GogoKit;

namespace ExternalApiServices
{
    public class VenueRetrieve : IVenueRetriever
    {

        private const string CLIENT_ID = "";
        private const string CLIENT_SECRET = "";
        private ViagogoClient _client;

        public VenueRetrieve()
        {
            _client = new ViagogoClient(CLIENT_ID,
                              CLIENT_SECRET,
                              new ProductHeaderValue("AwesomeApp", "1.0"));

            var token = await _client.OAuth2.GetAccessTokenAsync();
            await client.TokenStore.SetTokenAsync(token);
        }




       


    }

    public interface IVenueRetriever
    {
    }
}
