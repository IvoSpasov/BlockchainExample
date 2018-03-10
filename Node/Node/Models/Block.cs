namespace Node.Models
{
    using System;
    using Newtonsoft.Json;

    public class Block : BlockCandidate
    {
        [JsonProperty(Order = 1)]
        public int Nonce { get; set; }

        [JsonProperty(Order = 2)]
        public DateTime DateCreated { get; set; }

        [JsonProperty(Order = 3)]
        public string BlockHash { get; set; }
    }
}
