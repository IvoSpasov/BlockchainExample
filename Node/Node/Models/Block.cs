namespace Node.Models
{
    using System;

    public class Block : BlockCandidate
    {
        public int Nonce { get; set; }

        public DateTime DateCreated { get; set; }

        public string BlockHash { get; set; }
    }
}
