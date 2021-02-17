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

        [Route("Add")]
        [HttpPost]
        public IActionResult Add(string payerName, int points, DateTime timestamp)
        {
            _service.Add(new TransactionRecord { PayerName = payerName, Points = points, Timestamp = timestamp });
            return Ok();
        }

        [Route("PayerBalance")]
        [HttpGet]
        public IActionResult GetPayerBalance()
        {
            return Ok(_service.GetPayerBalance());
        }

        [Route("UserBalance")]
        [HttpGet]
        public IActionResult GetUserBalance()
        {
            return Ok(_service.GetUserBalance());
        }

        [Route("Remove")]
        [HttpPost]
        public IActionResult RemoveAll()
        {
            _service.RemoveAll();
            return Ok();
        }

        [Route("Spend")]
        [HttpPost]
        public IActionResult Spend(int points)
        {
            return Ok(_service.Spend(points));
        }
    }
}
