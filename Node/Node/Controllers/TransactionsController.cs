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

        [HttpPost("send")]
        public string Send([FromBody]TransactionVM transactionVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid input");

                transactionService.Process(transactionVM);
            }
            catch (Exception ex)
            {
                return $"Unable to add transaction to node: {ex}";
            }

            return "Transaction sucessfully added to node.";
        }
    }
}