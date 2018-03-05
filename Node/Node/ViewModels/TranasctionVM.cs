namespace Node.ViewModels
{
    using System;

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
