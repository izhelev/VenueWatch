using System.Collections.Generic;
using System.Web.Http;
using BLL;
using Data;
using Entities;

namespace VenueWatch.Controllers
{
    public class VenueWatchItemController : ApiController
    {
        private readonly IVenueWatchRepository _venueWatchRepository;
        private readonly ICurrentUserRetriever _currentUserRetriever;

        public VenueWatchItemController(IVenueWatchRepository venueWatchRepository, ICurrentUserRetriever currentUserRetriever)
        {
            _venueWatchRepository = venueWatchRepository;
            _currentUserRetriever = currentUserRetriever;
        }

        public IEnumerable<IVenueWatchRepository> Get()
        {
            return _venueWatchRepository.GetByUserId(_currentUserRetriever.GetCurrentUserId());
        }

        public VenueWatchItem Get(int id)
        {
            return _venueWatchRepository.Get(id);
        }

        public int PostTodoItem(VenueWatchItem venueWatchItem)
        {
            return _venueWatchRepository.Save(venueWatchItem);

        }

        public void PutItem(int id, VenueWatchItem venueWatchItem)
        {
            _venueWatchRepository.Save(venueWatchItem);
        }     
    }
}
