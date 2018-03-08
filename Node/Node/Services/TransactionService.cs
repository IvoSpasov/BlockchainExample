namespace Node.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Node.ApiModels;
    using Node.Interfaces;
    using Node.Models;
    using Node.Utilities;

    public class TransactionService : ITransactionService
    {
        private List<PendingTransaction> pendingTransactions;
        private List<ConfirmedTransaction> confirmedTransactions;

        public TransactionService()
        {
            this.pendingTransactions = new List<PendingTransaction>();
            this.confirmedTransactions = new List<ConfirmedTransaction>();
        }

        public List<PendingTransaction> PendingTransactions
        {
            get { return this.pendingTransactions; }
        }

        public List<ConfirmedTransaction> ConfirmedTransactions
        {
            get { return this.confirmedTransactions; }
        }

        public PendingTransaction GetTransaction(string tranHash)
        {
            var foundPendingTran = pendingTransactions.FirstOrDefault(t => t.Hash == tranHash);
            if (foundPendingTran == null)
                throw new Exception("Transaction not found.");

            return foundPendingTran;
        }

        public void ProcessNewIncomingTransaction(TransactionRequestModel tranRM)
        {
            var newTransaction = Create(tranRM);
            newTransaction.Hash = Crypto.CalculateSHA256ToString(newTransaction.AsJsonWithoutHash());
            this.Validate(newTransaction);
            this.pendingTransactions.Add(newTransaction);
        }

        public List<ConfirmedTransaction> CreateConfirmedTransactions(int nextBlockIndex)
        {
            List<PendingTransaction> pendingTransactions = this.pendingTransactions;
            var candidatesForConfirmedTransactions = pendingTransactions
                .Select(pt => new ConfirmedTransaction(pt)
                {
                    MinedInBlockIndex = nextBlockIndex,
                    TransferSuccessful = true
                })
                .ToList();

            return candidatesForConfirmedTransactions;
        }

        private void Validate(PendingTransaction currentTransaction)
        {
            if (pendingTransactions.Any(t => t.Hash == currentTransaction.Hash))
                throw new Exception("Transaction is already added.");

            if (!Crypto.IsSignatureValid(currentTransaction))
                throw new Exception("Transaction signature is not valid.");

            //Check for missing / invalid fields            
            //Check for correct balances?
        }

        private PendingTransaction Create(TransactionRequestModel tranRM)
        {
            var newTransaction = new PendingTransaction
            {
                From = tranRM.From,
                To = tranRM.To,
                SenderPublicKey = tranRM.SenderPubKey,
                Value = tranRM.Value,
                Fee = tranRM.Fee,
                DateCreated = tranRM.DateCreated,
                SenderSignature = tranRM.SenderSignature
            };

            return newTransaction;
        }
    }
}
