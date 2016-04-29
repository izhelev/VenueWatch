using System.Threading.Tasks;
using System.Web.Http;
using GogoKit;
using GogoKit.Models.Response;

namespace VenueWatch.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IViagogoClient _viagogoClient;

        public EventsController(IViagogoClient viagogoClient)
        {
            _viagogoClient = viagogoClient;
        }

        public async Task<Event> Get(int id)
        {
            var result = await _viagogoClient.Events.GetAsync(id);
            return result;
        }
    }
}
