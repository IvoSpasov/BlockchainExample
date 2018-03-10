namespace Node.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Node.Interfaces;
    using Node.Models;

    [Produces("application/json")]
    [Route("api/Address")]
    public class AddressController : Controller
    {
        private IBlockService blockService;
        private ITransactionService transactionService;

        public AddressController(IBlockService blockService, ITransactionService transactionService)
        {
            this.blockService = blockService;
            this.transactionService = transactionService;
        }

        [HttpGet("{address}/transactions")]
        public IActionResult GetAllTransactionsPerAddress(string address)
        {
            //TODO: validate address
            try
            {
                var pendingTran = this.transactionService.GetPendingTransactions(address);
                var confirmedTran = this.blockService.GetConfirmedTransactions(address);
                if (pendingTran == null && confirmedTran == null)
                {
                    return NotFound("No transactions found for this address");
                }

                //TODO: the code below must be added to a separate service
                var allTransactions = new List<PendingTransaction>();
                allTransactions.AddRange(pendingTran);
                allTransactions.AddRange(confirmedTran);
                var orderedTransactions =  allTransactions.OrderByDescending(t => t.DateCreated);
                return Json(orderedTransactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Couldn't get transactions for this address: {ex}");
            }
        }
    }
}