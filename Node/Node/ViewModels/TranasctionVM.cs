namespace Node.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TransactionVM
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string SenderPubKey { get; set; }

        public int Value { get; set; }

        public int Fee { get; set; }

        public DateTime DateCreated { get; set; }

        public string[] SenderSignature { get; set; }
    }
}
