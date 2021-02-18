using Microsoft.AspNetCore.Mvc;
using PointTracker.Core.Models;
using PointTracker.Core.Services;
using System;

namespace PointTracker.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointTrackerController : ControllerBase
    {
        private readonly ITransactionService _service;

        public PointTrackerController(ITransactionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds a transaction to the list of transactions.
        /// </summary>
        /// <param name="payerName">The name of the payer.</param>
        /// <param name="points">The number of points to add.</param>
        /// <param name="timestamp">The ISO 8601 timestamp of the transaction.</param>
        [Route("Add")]
        [HttpPost]
        public IActionResult Add(string payerName, int points, DateTime timestamp)
        {
            _service.Add(new TransactionRecord { PayerName = payerName, Points = points, Timestamp = timestamp });
            return Ok();
        }

        /// <summary>
        /// Gets the amount of points per payer.
        /// </summary>
        [Route("PayerBalance")]
        [HttpGet]
        public IActionResult GetPayerBalance()
        {
            return Ok(_service.GetPayerBalance());
        }

        /// <summary>
        /// Gets the amount of points the user has available.
        /// </summary>
        [Route("UserBalance")]
        [HttpGet]
        public IActionResult GetUserBalance()
        {
            return Ok(_service.GetUserBalance());
        }

        /// <summary>
        /// Removes all of the transactions in the list.
        /// </summary>
        [Route("Remove")]
        [HttpPost]
        public IActionResult RemoveAll()
        {
            _service.RemoveAll();
            return Ok();
        }

        /// <summary>
        /// Spends the given number of points in chronological order of the transactions.
        /// </summary>
        /// <param name="points">The number of points to spend.</param>
        [Route("Spend")]
        [HttpPost]
        public IActionResult Spend(int points)
        {
            return Ok(_service.Spend(points));
        }
    }
}
