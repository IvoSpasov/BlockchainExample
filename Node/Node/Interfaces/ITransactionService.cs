namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.ApiModels;
    using Node.Models;

    public interface ITransactionService
    {
        List<Transaction> PendingTransactions { get; }

        List<ConfirmedTransaction> ConfirmedTransactions { get; }

        Transaction GetTransaction(string tranHash);

        void ProcessNewIncomingTransaction(TransactionRequestModel tranRM);

        List<ConfirmedTransaction> CreateConfirmedTransactions(int nextBlockIndex);
    }
}
