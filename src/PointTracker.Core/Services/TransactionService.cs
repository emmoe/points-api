using PointTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointTracker.Core.Services
{
    public class TransactionService
    {
        public IList<TransactionRecord> transactions;

        public TransactionService()
        {
            transactions = new List<TransactionRecord>();
        }

        public TransactionService(IList<TransactionRecord> transactions)
        {
            this.transactions = transactions;
        }

        public void Add(TransactionRecord transaction)
        {
            transactions.Add(transaction);
        }

        public SpendResponse Spend(int points)
        {
            throw new NotImplementedException();
        }

        public int GetUserBalance()
        {
            return transactions.Sum(t => t.Points);
        }

        public Dictionary<string, int> GetPayerBalance()
        {
            return transactions
                .GroupBy(x => x.PayerName)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Points));
        }
    }
}
