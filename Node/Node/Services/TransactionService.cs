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
        private List<Transaction> validPendingTransactions;

        public TransactionService()
        {
            this.validPendingTransactions = new List<Transaction>();
        }

        public void Process(TransactionVM tranVM)
        {
            var newTransaction = Create(tranVM);
            newTransaction.Hash = Crypto.Sha256(newTransaction.AsJsonString(true));
            this.Validate(newTransaction);
            this.validPendingTransactions.Add(newTransaction);
        }

        private void Validate(Transaction currentTransaction)
        {
            //Check for collisions -> duplicated transactions are skipped
            if (validPendingTransactions.Any(t => t.Hash == currentTransaction.Hash))
            {
                throw new Exception("Transaction already added");
            }

            //Check for missing / invalid fields
            //Validate the transaction signature
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
