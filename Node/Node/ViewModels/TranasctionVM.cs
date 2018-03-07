namespace Node.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TransactionVM
    {
        private const int addressLength = 40;
        private const int publicKeyLength = 66;

        [Required]
        [StringLength(addressLength, MinimumLength = addressLength)]
        public string From { get; set; }

        [Required]
        [StringLength(addressLength, MinimumLength = addressLength)]
        public string To { get; set; }

        [Required]
        [StringLength(publicKeyLength, MinimumLength = publicKeyLength)]
        public string SenderPubKey { get; set; }

        public int Value { get; set; }

        public int Fee { get; set; }

        public DateTime DateCreated { get; set; }

        public string[] SenderSignature { get; set; }
    }
}
