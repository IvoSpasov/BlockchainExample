namespace Node.Models
{
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

        public int? MinedInBlockIndex { get; set; }

        public bool TransferSuccessful { get; set; }
    }
}
