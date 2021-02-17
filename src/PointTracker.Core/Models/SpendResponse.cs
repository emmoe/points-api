using System.Collections.Generic;

namespace PointTracker.Core.Models
{
    public record SpendResponse
    {
        public IEnumerable<SpendResult> Results { get; init; }
    }
}
