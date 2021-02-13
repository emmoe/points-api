using PointTracker.Core.Models;
using PointTracker.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PointTracker.Tests.Services
{
    public class TransactionServiceUnitTests
    {
        [Fact]
        public void Test()
        {
            var transactionHistory = new List<TransactionRecord>
            {
                new TransactionRecord {PayerName = "Dannon", Points= 1000, Timestamp = new DateTime(2020, 11, 02, 14, 0, 0)},
                new TransactionRecord {PayerName = "Unilever", Points= 200, Timestamp = new DateTime(2020, 10, 31, 11, 0, 0)},
                new TransactionRecord {PayerName = "Dannon", Points= -200, Timestamp = new DateTime(2020, 10, 31, 15, 0, 0)},
                new TransactionRecord {PayerName = "Miller Coors", Points= 10000, Timestamp = new DateTime(2020, 11, 01, 14, 0, 0)},
                new TransactionRecord {PayerName = "Dannon", Points= 300, Timestamp = new DateTime(2020, 10, 31, 10, 0, 0)}
            };
            var subject = new TransactionService(transactionHistory);
            var results = subject.GetPayerBalance();
        }
    }
}
