using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Node.ViewModels
{
    public class TransactionVM
    {
        public string From { get; set; }

        public string To { get; set; }

        public string SenderPubKey { get; set; }

        public int Value { get; set; }

        public int Fee { get; set; }

        public DateTime DateCreated { get; set; }

        public string[] SenderSignature { get; set; }
    }
}
