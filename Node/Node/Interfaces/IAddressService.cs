namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.Models;

    public interface IAddressService
    {
        IEnumerable<PendingTransaction> GetAllTransactions(string address);

        long GetPendingBalance(string address, IEnumerable<PendingTransaction> transactions);
    }
}
