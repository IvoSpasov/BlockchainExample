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
        private IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet("{address}/transactions")]
        public IActionResult GetAllTransactionsPerAddress(string address)
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
    }
}