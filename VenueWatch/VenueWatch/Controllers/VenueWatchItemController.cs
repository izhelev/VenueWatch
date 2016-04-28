using System;
using System.Collections.Generic;
using System.Web.Http;

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

        // GET api/<controller>
        public IEnumerable<IVenueWatchRepository> Get()
        {
            return _venueWatchRepository.GetByUserId(_currentUserRetriever.GetCurrentUserId());
        }

        // GET api/<controller>/5
        public VenueWatchItem Get(int id)
        {
            return _venueWatchRepository.Get(id);
        }

        // POST api/<controller>
        public int PostTodoItem(VenueWatchItem venueWatchItem)
        {
            return _venueWatchRepository.Save(venueWatchItem);

        }

        // PUT api/<controller>/5
        public void PutItem(int id, VenueWatchItem venueWatchItem)
        {
            _venueWatchRepository.Save(venueWatchItem);
        }     
    }

    public interface ICurrentUserRetriever
    {
        Guid GetCurrentUserId();
    }

    public class VenueWatchItem
    {
        public int VenueId { get; set; }
    }

    public interface IVenueWatchRepository
    {
        IEnumerable<IVenueWatchRepository> GetByUserId(Guid userId);
        VenueWatchItem Get(int id);
        int Save(object venueWatchItem);
    }
}
