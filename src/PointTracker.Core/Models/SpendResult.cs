﻿namespace PointTracker.Core.Models
{
    public record SpendResult
    {
        public string PayerName { get; init; }
        public int Points { get; init; }
    }
}
