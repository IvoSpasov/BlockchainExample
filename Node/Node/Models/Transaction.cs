using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Node.Models
{
    public class Transaction
    {
        public string From { get; set; }

        public string To { get; set; }

        public string SenderPublicKey { get; set; }

        public int Value { get; set; }

        public int Fee { get; set; }

        public DateTime DateCreated { get; set; }

        public string[] SenderSignature { get; set; }

        public string TransactionHash { get; set; }

        public int? MinedInBlockIndex { get; set; }

        public bool TransferSuccessful { get; set; }
    }
}
