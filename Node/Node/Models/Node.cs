namespace Node.Models
{
    using System.Collections.Generic;

    public class Node
    {
        public string[] Peers { get; set; }

        public Block[] Blocks { get; set; }

        public Transaction[] PendingTransactions { get; set; }

        public int Difficulty { get; set; }

        public Dictionary<string, BlockCandidate> BlockCandidates { get; set; }
    }
}
