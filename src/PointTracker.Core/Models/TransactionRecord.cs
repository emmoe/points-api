using System;

namespace PointTracker.Core.Models
{
    public record TransactionRecord
    {
        public string PayerName { get; init; }
        public DateTime Timestamp { get; init; }
        public int Points { get; init; }
    }
}
