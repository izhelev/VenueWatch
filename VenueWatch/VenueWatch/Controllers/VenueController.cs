using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GogoKit;
using GogoKit.Models.Request;
using GogoKit.Models.Response;

namespace VenueWatch.Controllers
{
    public class VenueController : ApiController
    {
        private readonly IViagogoClient _viagogoClient;

        public VenueController(IViagogoClient viagogoClient)
        {
            _viagogoClient = viagogoClient;
        }

        public async Task<IEnumerable<Venue>> GetByName(string name)
        {
            var searchRequest = new SearchResultRequest()
            {   
                Embed = new List<SearchResultEmbed>(){ SearchResultEmbed.Venue} ,
                Type = new List<SearchResultTypeFilter>()
                {
                    SearchResultTypeFilter.Venue
                }
            };
            var results = await _viagogoClient.Search.GetAsync(name, searchRequest);
            return results.Items.Select(i => i.Venue);
        }
    }
}
