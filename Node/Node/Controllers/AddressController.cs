namespace Node.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Node.ApiModels;
    using Node.Interfaces;
    using Node.Services;

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
                bool anyPendingTran = this.addressService.CanGetBalance(address, BalanceType.Pending, out long pendingBalance);
                if (!anyPendingTran)
                {
                    return NotFound("No transactions found for this address.");
                }

                bool anyLastMinedTran = this.addressService.CanGetBalance(address, BalanceType.LastMined, out long lastMinedBalance);
                // confirmed by specific block count
                bool anyConfirmedTran = this.addressService.CanGetBalance(address, BalanceType.Confrimed, out long confirmedBalance);
                var balanceForAddresRM = new BalanceForAddressResponseModel(
                    address, 
                    anyPendingTran, pendingBalance,
                    anyLastMinedTran, lastMinedBalance,
                    anyConfirmedTran, confirmedBalance);

                return Json(balanceForAddresRM);
            }
            catch (Exception ex)
            {
                return BadRequest($"Couldn't get balance for this address: {ex}");
            }
        }
    }
}