namespace Node.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Node.ApiModels;
    using Node.Interfaces;

    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private ITransactionService transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet("{hash}")]
        public IActionResult GetTransaction(string hash)
        {
            try
            {
                var foundTransaction = transactionService.GetTransaction(hash);
                return Json(foundTransaction);
            }
            catch (Exception ex)
            {
                return NotFound($"Transaction not found: {ex}");
            }
        }

        [HttpGet("pending")]
        public IActionResult GetPendingTransactions()
        {
            try
            {
                var pendingTranactions = this.transactionService.PendingTransactions;
                return Json(pendingTranactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Couldn't get pending transactions: {ex}");
            }
        }

        [HttpGet("confirmed")]
        public IActionResult GetConfirmedTransactions()
        {
            try
            {
                var confirmedTranactions = this.transactionService.ConfirmedTransactions;
                return Json(confirmedTranactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Couldn't get confirmed transactions: {ex}");
            }
        }

        [HttpPost("send")]
        public IActionResult Send([FromBody]TransactionRequestModel transactionRM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid input: " + ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);

                transactionService.ProcessNewIncomingTransaction(transactionRM);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMsg = $"Unable to add transaction to node: {ex}" });
            }

            return Ok("Transaction sucessfully added to node");
        }
    }
}