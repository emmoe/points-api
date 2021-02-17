using PointTracker.Core.Models;

namespace PointTracker.Core.Services
{
    public interface ITransactionService
    {
        void Add(TransactionRecord transaction);
        SpendResponse GetPayerBalance();
        int GetUserBalance();
        void RemoveAll();
        SpendResponse Spend(int points);
    }
}
