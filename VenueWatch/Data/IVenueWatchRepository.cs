using System;
using System.Collections.Generic;
using Entities;

namespace Data
{
    public interface IVenueWatchRepository
    {
        IEnumerable<IVenueWatchRepository> GetByUserId(Guid userId);
        VenueWatchItem Get(int id);
        int Save(object venueWatchItem);
    }
}