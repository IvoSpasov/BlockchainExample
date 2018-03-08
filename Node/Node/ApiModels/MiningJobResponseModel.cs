namespace Node.Models
{
    using Newtonsoft.Json;

    public class MiningJobResponseModel
    {
        [JsonProperty("index")]
        public int BlockIndex { get; set; }

        public int TransactionsIncluded { get; set; }

        public int ExpectedReward { get; set; }

        public string RewardAddress { get; set; }

        public string BlockDataHash { get; set; }

        public int Difficulty { get; set; }
    }
}
