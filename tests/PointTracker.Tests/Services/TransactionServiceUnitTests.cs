using FluentAssertions;
using PointTracker.Core.Models;
using PointTracker.Core.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace PointTracker.Tests.Services
{
    public class TransactionServiceUnitTests
    {
        private readonly List<TransactionRecord> transactions = new List<TransactionRecord>
            {
                new TransactionRecord {PayerName = "Dannon", Points= 1000, Timestamp = new DateTime(2020, 11, 02, 14, 0, 0)},
                new TransactionRecord {PayerName = "Dannon", Points= 300, Timestamp = new DateTime(2020, 10, 31, 10, 0, 0)},
                new TransactionRecord {PayerName = "Unilever", Points= 200, Timestamp = new DateTime(2020, 10, 31, 11, 0, 0)},
                new TransactionRecord {PayerName = "Miller Coors", Points= 10000, Timestamp = new DateTime(2020, 11, 01, 14, 0, 0)},
                new TransactionRecord {PayerName = "Dannon", Points= -200, Timestamp = new DateTime(2020, 10, 31, 15, 0, 0)},
            };

        [Fact]
        public void WhenSpendIsCalledWithSufficientPoints()
        {
            var subject = new TransactionService(transactions);
            var response = subject.Spend(5000);
            var expected = new SpendResponse
            {
                Results = new List<SpendResult> {
                new SpendResult { PayerName = "Dannon", Points = -100 },
                new SpendResult { PayerName = "Unilever", Points = -200 },
                new SpendResult { PayerName = "Miller Coors", Points = -4700 },
            }
            };
            response.Results.Should().BeEquivalentTo(expected.Results, because: "a total of 5000 points was deducted from these payers");
        }

        [Fact]
        public void WhenSpendIsCalledWithInsufficientPoints()
        {
            var subject = new TransactionService(transactions);
            var response = subject.Spend(20000);
            var expected = new SpendResponse
            {
                Results = new List<SpendResult>
                {
                    new SpendResult { PayerName = "Dannon", Points = -1100 },
                    new SpendResult { PayerName = "Unilever", Points = -200 },
                    new SpendResult { PayerName = "Miller Coors", Points = -10000 },
                }
            };
            response.Results.Should().BeEquivalentTo(expected.Results, because: "it deducted the maximum available points for each payer, but did not go negative");
        }

        [Fact]
        public void WhenUserBalanceIsCalled()
        {

            var subject = new TransactionService(transactions);
            var response = subject.GetUserBalance();
            response.Should().Be(11300, because: "that is the sum of all the points");
        }

        [Fact]
        public void WhenPayerBalanceIsCalledWithPositivePointValues()
        {
            var subject = new TransactionService(transactions);
            var response = subject.GetPayerBalance();
            var expected = new SpendResponse
            {
                Results = new List<SpendResult>
                {
                    new SpendResult { PayerName = "Dannon", Points = 1100 },
                    new SpendResult { PayerName = "Unilever", Points = 200 },
                    new SpendResult { PayerName = "Miller Coors", Points = 10000 },
                }
            };
            response.Results.Should().BeEquivalentTo(expected.Results, because: "that is the sum of points available per payer");
        }

        [Fact]
        public void WhenPayerBalanceIsCalledWithNegativePointValues()
        {
            var negativeTransactionTotal = new List<TransactionRecord>
            {
                new TransactionRecord {PayerName = "Dannon", Points= -1000, Timestamp = new DateTime(2020, 11, 02, 14, 0, 0)},
                new TransactionRecord {PayerName = "Dannon", Points= 300, Timestamp = new DateTime(2020, 10, 31, 10, 0, 0)},
                new TransactionRecord {PayerName = "Unilever", Points= -500, Timestamp = new DateTime(2020, 10, 31, 11, 0, 0)},
                new TransactionRecord {PayerName = "Miller Coors", Points= -1000, Timestamp = new DateTime(2020, 11, 01, 14, 0, 0)},
            };
            var subject = new TransactionService(negativeTransactionTotal);
            var response = subject.GetPayerBalance();
            var expected = new SpendResponse
            {
                Results = new List<SpendResult>
                {
                    new SpendResult { PayerName = "Dannon", Points = 0 },
                    new SpendResult { PayerName = "Unilever", Points = 0 },
                    new SpendResult { PayerName = "Miller Coors", Points = 0 },
                }
            };
            response.Results.Should().BeEquivalentTo(expected.Results, because: "no payer has negative points");
        }

        [Fact]
        public void WhenAddIsCalled()
        {
            var subject = new TransactionService(transactions);
            subject.Add(new TransactionRecord { PayerName = "Test", Points = 100, Timestamp = DateTime.Now });
            subject._transactions.Should().HaveCount(6, because: "a new transaction was added to the list");
        }

        [Fact]
        public void WhenRemoveAllIsCalled()
        {
            var subject = new TransactionService(transactions);
            subject.RemoveAll();
            subject._transactions.Should().HaveCount(0, because: "all transactions were remoted from the list");
        }
    }
}
