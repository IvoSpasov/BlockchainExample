using Node.Models;
using Node.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Node.Services
{
    public class TransactionService
    {
        private List<Transaction> validPendingTransactions;

        public TransactionService()
        {
            this.validPendingTransactions = new List<Transaction>();
        }
        
        public void Validate()
        {
            //Checks for collisions -> duplicated transactions are skipped
            //Checks for missing / invalid fields
            //Validates the transaction signature
            //Checks for correct balances?
            //Puts the transaction in the "pending transactions" pool

        }
        public void Add()
        {
            // create
            // sing (sining should be done by the wallet)
            // validate
            // add to valid transactions
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

        private void CalculateHash(TransactionVM tranVM)
        {
            
        }
    }
}
