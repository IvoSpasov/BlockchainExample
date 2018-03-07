namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.Models;
    using Node.ViewModels;

    public interface ITransactionService
    {
        List<Transaction> PendingTransactions { get; }

        List<ConfirmedTransaction> ConfirmedTransactions { get; }

        Transaction GetTransaction(string tranHash);

        void ProcessNewIncomingTransaction(TransactionVM tranVM);

        List<ConfirmedTransaction> CreateConfirmedTransactions(int nextBlockIndex);
    }
}
