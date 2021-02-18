using PointTracker.Core.Models;
using System.Collections.Generic;

namespace PointTracker.Core.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// Adds a transaction to the list of transactions.
        /// </summary>
        /// <param name="transaction">The <see cref="TransactionRecord"/> to add.</param>
        void Add(TransactionRecord transaction);

        /// <summary>
        /// Gets the amount of points per payer.
        /// </summary>
        /// <returns>An IEnumerable of <see cref="SpendResult"/> for each payer in the list of transactions.</returns>
        IEnumerable<SpendResult> GetPayerBalance();

        /// <summary>
        /// Gets the amount of points the user has available.
        /// </summary>
        /// <returns>The point value.</returns>
        int GetUserBalance();

        /// <summary>
        /// Removes all of the transactions in the list.
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Spends the given number of points in chronological order of the transactions.
        /// </summary>
        /// <param name="points">The number of points to spend.</param>
        /// <returns>An IEnumerable of <see cref="SpendResult"/> indicating the number of points used per payer.</returns>
        IEnumerable<SpendResult> Spend(int points);
    }
}
