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
        private IBlockService blockService;
        private ITransactionService transactionService;

        public TransactionsController(IBlockService blockService, ITransactionService transactionService)
        {
            this.blockService = blockService;
            this.transactionService = transactionService;
        }

        [HttpGet("{hash}")]
        public IActionResult GetTransaction(string hash)
        {
            try
            {
                var foundPendingTran = transactionService.GetPendingTransaction(hash);
                if (foundPendingTran != null)
                {
                    return Json(foundPendingTran);
                }

                var foundConfirmedTran = blockService.GetConfrimedTransaction(hash);
                if (foundConfirmedTran != null)
                {
                    return Json(foundConfirmedTran);
                }

                return NotFound($"Transaction not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex}");
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