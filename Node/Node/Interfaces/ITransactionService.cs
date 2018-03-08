namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.ApiModels;
    using Node.Models;

    public interface ITransactionService
    {
        List<PendingTransaction> PendingTransactions { get; }

        List<ConfirmedTransaction> ConfirmedTransactions { get; }

        PendingTransaction GetTransaction(string tranHash);

        void ProcessNewIncomingTransaction(TransactionRequestModel tranRM);

        List<ConfirmedTransaction> CreateConfirmedTransactions(int nextBlockIndex);
    }
}
