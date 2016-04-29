using System;
using System.Collections.Generic;
using System.Web.Http;

namespace VenueWatch.Controllers
{
    [Authorize]
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

    public interface ICurrentUserRetriever
    {
        Guid GetCurrentUserId();
    }

    public class CurrentUserRetriever : ICurrentUserRetriever
    {
        public Guid GetCurrentUserId()
        {
            throw new NotImplementedException();
        }
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

    public class VenueWatchRepository : IVenueWatchRepository
    {
        public IEnumerable<IVenueWatchRepository> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public VenueWatchItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(object venueWatchItem)
        {
            throw new NotImplementedException();
        }
    }
}
