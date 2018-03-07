namespace Node.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Node.Interfaces;
    using Node.Models;
    using Node.Utilities;
    using Node.ViewModels;

    public class TransactionService : ITransactionService
    {
        private List<Transaction> pendingTransactions;
        private List<ConfirmedTransaction> confirmedTransactions;

        public TransactionService()
        {
            this.pendingTransactions = new List<Transaction>();
            this.confirmedTransactions = new List<ConfirmedTransaction>();
        }

        public List<Transaction> PendingTransactions
        {
            get { return this.pendingTransactions; }
        }

        public List<ConfirmedTransaction> ConfirmedTransactions
        {
            get { return this.confirmedTransactions; }
        }

        public Transaction GetTransaction(string tranHash)
        {
            var foundPendingTran = pendingTransactions.FirstOrDefault(t => t.Hash == tranHash);
            if (foundPendingTran == null)
                throw new Exception("Transaction not found.");

            return foundPendingTran;
        }

        public void ProcessNewIncomingTransaction(TransactionVM tranVM)
        {
            var newTransaction = Create(tranVM);
            newTransaction.Hash = Crypto.CalculateSHA256ToString(newTransaction.AsJsonString(true));
            this.Validate(newTransaction);
            this.pendingTransactions.Add(newTransaction);
        }

        private void Validate(Transaction currentTransaction)
        {
            if (pendingTransactions.Any(t => t.Hash == currentTransaction.Hash))
                throw new Exception("Transaction is already added.");

            if (!Crypto.IsSignatureValid(currentTransaction))
                throw new Exception("Transaction signature is not valid.");

            //Check for missing / invalid fields            
            //Check for correct balances?
        }

        private Transaction Create(TransactionVM tranVM)
        {
            var newTransaction = new Transaction
            {
                From = tranVM.From,
                To = tranVM.To,
                SenderPublicKey = tranVM.SenderPubKey,
                Value = tranVM.Value,
                Fee = tranVM.Fee,
                DateCreated = tranVM.DateCreated,
                SenderSignature = tranVM.SenderSignature
            };

            return newTransaction;
        }
    }
}
