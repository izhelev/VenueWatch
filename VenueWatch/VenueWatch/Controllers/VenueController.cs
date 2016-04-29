using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GogoKit;
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

        public async Task<IEnumerable<Venue>> Get()
        {
            return await _viagogoClient.Venues.GetAllAsync();
        }

        public async Task<IEnumerable<Venue>> GetByName(string name)
        {
            var results = await _viagogoClient.Venues.GetAllAsync();
            return results.Where(v => v.Name.Contains(name));
        }
    }
}
