namespace Node.Models
{
    using System.Collections.Generic;

    public class BlockCandidate
    {
        public int Index { get; set; }

        public List<ConfirmedTransaction> ConfirmedTransactions { get; set; }

        public int Difficulty { get; set; }

        public string PreviousBlockHash { get; set; }
        
        // miner address
        public string MinedBy { get; set; }

        // merkle tree?
        public string BlockDataHash { get; set; }
    }
}
