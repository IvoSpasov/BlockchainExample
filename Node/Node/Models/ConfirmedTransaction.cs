namespace Node.Models
{
    using Newtonsoft.Json;

    public class ConfirmedTransaction : Transaction
    {
        public ConfirmedTransaction(Transaction transaction)
        {
            this.From = transaction.From;
            this.To = transaction.To;
            this.SenderPublicKey = transaction.SenderPublicKey;
            this.Value = transaction.Value;
            this.Fee = transaction.Fee;
            this.DateCreated = transaction.DateCreated;
            this.SenderSignature = transaction.SenderSignature;
            this.Hash = transaction.Hash;
        }

        [JsonProperty(Order = 1)]
        public int? MinedInBlockIndex { get; set; }

        [JsonProperty(Order = 2)]
        public bool TransferSuccessful { get; set; }
    }
}
