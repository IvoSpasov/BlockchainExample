namespace Node.ApiModels
{
    using System;

    public class MiningJobRequestModel
    {
        public string MinerAddress { get; set; }

        public string BlockDataHash { get; set; }

        public DateTime DateCreated { get; set; }

        public int Nonce { get; set; }

        public string BlockHash { get; set; }
    }
}
