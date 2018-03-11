namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.Models;
    using Node.Services;

    public interface IAddressService
    {
        IEnumerable<PendingTransaction> GetAllTransactions(string address);

        bool CanGetBalance(string address, BalanceType balanceType, out long calculatedBalance);
    }
}
