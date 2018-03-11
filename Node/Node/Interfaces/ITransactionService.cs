namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.ApiModels;
    using Node.Models;

    public interface ITransactionService
    {
        List<PendingTransaction> PendingTransactions { get; }

        PendingTransaction GetPendingTransaction(string tranHash);

        IEnumerable<PendingTransaction> GetPendingTransactions(string address);

        void ProcessNewIncomingTransaction(TransactionRequestModel tranRM);

        List<ConfirmedTransaction> CreateConfirmedTransactions(int nextBlockIndex);

        void ClearAllAddedToBlockPendingTransactions(List<ConfirmedTransaction> addedToBlockPT);

        ConfirmedTransaction CreateMinerTransaction(string minerAddress, int nextBlockIndex);
    }
}
