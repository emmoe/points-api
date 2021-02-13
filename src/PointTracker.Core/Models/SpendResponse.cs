using System.Collections.Generic;

namespace PointTracker.Core.Models
{
    public record SpendResponse
    {
        public IList<SpendResult> Results { get; init; }
    }
}
