using System;
using System.Collections.Generic;
using Entities;

namespace Data
{
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