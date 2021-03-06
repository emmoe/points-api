﻿using PointTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointTracker.Core.Services
{
    public class TransactionService : ITransactionService
    {
        public IList<TransactionRecord> _transactions;

        public TransactionService()
        {
            _transactions = new List<TransactionRecord>();
        }

        public TransactionService(IList<TransactionRecord> transactions)
        {
            _transactions = transactions;
        }

        /// <inheritdoc/>
        public void Add(TransactionRecord transaction)
        {
            _transactions.Add(transaction);
        }

        /// <inheritdoc/>
        public void RemoveAll()
        {
            _transactions = new List<TransactionRecord>();
        }

        /// <inheritdoc/>
        public IEnumerable<SpendResult> Spend(int points)
        {
            HandleNegativePoints();
            return DeductPoints(points, _transactions)
                .Select(t => new SpendResult { PayerName = t.Key, Points = t.Value });
        }

        /// <inheritdoc/>
        public int GetUserBalance() =>
            _transactions.Sum(t => t.Points);

        /// <inheritdoc/>
        public IEnumerable<SpendResult> GetPayerBalance() => _transactions
                .GroupBy(x => x.PayerName)
                .Select(t => new SpendResult { PayerName = t.Key, Points = Math.Max(t.Sum(p => p.Points), 0) });

        private void HandleNegativePoints()
        {
            var negativeTransactions = _transactions.Where(t => t.Points < 0).OrderBy(t => t.Timestamp);
            if (negativeTransactions.Any())
            {
                foreach (var nt in negativeTransactions)
                {
                    _transactions.Remove(nt);
                    var availableTransactions = _transactions.Where(t => t.PayerName == nt.PayerName && t.Timestamp < nt.Timestamp && t.Points > 0);
                    DeductPoints(Math.Abs(nt.Points), availableTransactions);
                }
            }
        }

        private Dictionary<string, int> DeductPoints(int points, IEnumerable<TransactionRecord> transactions)
        {
            var deductionTransactions = transactions.OrderBy(o => o.Timestamp);
            var pointsToDeduct = points;
            var deductions = new Dictionary<string, int>();
            foreach (var t in deductionTransactions)
            {
                _transactions.Remove(t);
                if (HasSufficientPoints(pointsToDeduct, t.Points))
                {
                    deductions[t.PayerName] = GetUpdatedPointDeduction(pointsToDeduct, deductions, t.PayerName);
                    _transactions.Add(t with { Points = t.Points - pointsToDeduct });
                    break;
                }
                else
                {
                    pointsToDeduct -= t.Points;
                    deductions[t.PayerName] = GetUpdatedPointDeduction(t.Points, deductions, t.PayerName);
                }
            }
            return deductions;
        }

        private static bool HasSufficientPoints(int pointsToDeduct, int existingPoints) => existingPoints >= pointsToDeduct;

        private static int GetUpdatedPointDeduction(int pointsToDeduct, Dictionary<string, int> deductions, string payer)
        {
            var payerPointsDeducted = pointsToDeduct * -1;
            if (deductions.TryGetValue(payer, out int value))
            {
                payerPointsDeducted += value;
            }
            return payerPointsDeducted;
        }
    }
}
