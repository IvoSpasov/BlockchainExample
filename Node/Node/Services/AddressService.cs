namespace Node.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Node.Interfaces;
    using Node.Models;

    public class AddressService : IAddressService
    {
        private const int blockConfirmationsCount = 6;
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

        public bool CanGetBalance(string address, BalanceType balanceType, out long calculatedBalance)
        {
            calculatedBalance = 0;
            // TODO: Filter transactions by "transfer successful"
            IEnumerable<PendingTransaction> transactions = null;
            if (balanceType == BalanceType.Pending)
            {
                transactions = this.GetAllTransactions(address);
                if (!transactions.Any())
                    return false;
            }
            else if(balanceType == BalanceType.LastMined)
            {
                transactions = this.blockService.GetConfirmedTransactions(address);
                if (!transactions.Any())
                    return false;
            }
            else if(balanceType == BalanceType.Confrimed)
            {
                transactions = this.blockService.GetConfrimedTransactions(address, blockConfirmationsCount);
                if (!transactions.Any())
                    return false;
            }

            foreach (var tran in transactions)
            {
                if (tran.To == address)
                {
                    calculatedBalance += tran.Value;
                }
                else if(tran.From == address)
                {
                    calculatedBalance -= tran.Value;
                    calculatedBalance -= tran.Fee;
                }
            }

            return true;
        }
    }

    public enum BalanceType
    {
        Confrimed,
        LastMined,
        Pending
    }
}
