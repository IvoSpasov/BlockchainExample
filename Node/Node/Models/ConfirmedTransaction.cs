namespace Node.Models
{
    public class ConfirmedTransaction : Transaction
    {
        public int? MinedInBlockIndex { get; set; }

        public bool TransferSuccessful { get; set; }
    }
}
