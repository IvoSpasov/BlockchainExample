namespace Node.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Node.Interfaces;
    using Node.Models;

    public class AddressService : IAddressService
    {
        private IBlockService blockService;
        private ITransactionService transactionService;

        public AddressService(IBlockService blockService, ITransactionService transactionService)
        {
            this.blockService = blockService;
            this.transactionService = transactionService;
        }

        public IEnumerable<PendingTransaction> GetAllTransactions(string address)
        {
            var pendingTran = this.transactionService.GetPendingTransactions(address);
            var confirmedTran = this.blockService.GetConfirmedTransactions(address);
            var allTransactions = new List<PendingTransaction>();
            allTransactions.AddRange(pendingTran);
            allTransactions.AddRange(confirmedTran);
            var orderedTransactions = allTransactions.OrderByDescending(t => t.DateCreated);
            return orderedTransactions;
        }

        public long GetPendingBalance(string address, IEnumerable<PendingTransaction> allTransactions)
        {
            long calculatedAmount = 0;
            // TODO: Filter transactions by "transfer successful"
            foreach (var tran in allTransactions)
            {
                if (tran.To == address)
                {
                    calculatedAmount += tran.Value;
                }
                else if(tran.From == address)
                {
                    calculatedAmount -= tran.Value;
                    calculatedAmount -= tran.Fee;
                }
            }

            return calculatedAmount;
        }
    }
}
