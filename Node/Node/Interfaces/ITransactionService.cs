namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.ApiModels;
    using Node.Models;

    public interface ITransactionService
    {
        List<PendingTransaction> PendingTransactions { get; }

        PendingTransaction GetPendingTransaction(string tranHash);

        void ProcessNewIncomingTransaction(TransactionRequestModel tranRM);

        List<ConfirmedTransaction> CreateConfirmedTransactions(int nextBlockIndex);

        void ClearAllAddedToBlockPendingTransactions(List<ConfirmedTransaction> addedToBlockPT);
    }
}
