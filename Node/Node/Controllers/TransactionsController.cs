namespace Node.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Node.Interfaces;
    using Node.ViewModels;

    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private ITransactionService transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet("hash")]
        public IActionResult Get(string hash)
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

        [HttpPost("send")]
        public IActionResult Send([FromBody]TransactionVM transactionVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid input");

                transactionService.Process(transactionVM);
            }
            catch (Exception ex)
            {
                return BadRequest($"Unable to add transaction to node: {ex}");
            }

            return Ok("Transaction sucessfully added to node");
        }
    }
}