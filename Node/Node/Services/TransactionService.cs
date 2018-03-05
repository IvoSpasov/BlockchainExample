namespace Node.Services
{
    using System.Collections.Generic;
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
            var transaction = Create(tranVM);
            transaction.TransactionHash = Crypto.Sha256(transaction.ToString());
            // validate
            // add to valid transactions
        }

        private void Validate()
        {
            //Checks for collisions -> duplicated transactions are skipped
            //Checks for missing / invalid fields
            //Validates the transaction signature
            //Checks for correct balances?
            //Puts the transaction in the "pending transactions" pool

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
