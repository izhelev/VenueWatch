using System;

namespace BLL
{
    public interface ICurrentUserRetriever
    {
        Guid GetCurrentUserId();
    }
}