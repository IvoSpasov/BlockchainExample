namespace Node.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Node.Interfaces;

    [Produces("application/json")]
    [Route("api/Address")]
    public class AddressController : Controller
    {
        private IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet("{address}/transactions")]
        public IActionResult GetAllTransactionsForAddress(string address)
        {
            //TODO: validate address
            try
            {
                var transactions = this.addressService.GetAllTransactions(address);
                if (!transactions.Any())
                {
                    return NotFound("No transactions found for this address.");
                }

                return Json(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Couldn't get transactions for this address: {ex}");
            }
        }

        [HttpGet("{address}/balance")]
        public IActionResult GetBalanceForAddress(string address)
        {
            //TODO: validate address
            try
            {
                var allTransactions = this.addressService.GetAllTransactions(address);
                if (!allTransactions.Any())
                {
                    return NotFound("No transactions found for this address.");
                }

                long pendingBalance = this.addressService.GetPendingBalance(address, allTransactions);
                return Json(new { address, pendingBalance });
            }
            catch (Exception ex)
            {
                return BadRequest($"Couldn't get balance for this address: {ex}");
            }
        }
    }
}