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

        public Transaction GetTransaction(string tranHash)
        {
            var foundTran = validPendingTransactions.FirstOrDefault(t => t.Hash == tranHash);
            if (foundTran == null)
                throw new Exception("Transaction not found.");

            return foundTran;
        }

        public void Process(TransactionVM tranVM)
        {
            var newTransaction = Create(tranVM);
            newTransaction.Hash = Crypto.CalculateSHA256ToString(newTransaction.AsJsonString(true));
            this.Validate(newTransaction);
            this.validPendingTransactions.Add(newTransaction);
        }

        private void Validate(Transaction currentTransaction)
        {
            if (validPendingTransactions.Any(t => t.Hash == currentTransaction.Hash))
                throw new Exception("Transaction is already added.");

            if(!Crypto.IsSignatureValid(currentTransaction))
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
