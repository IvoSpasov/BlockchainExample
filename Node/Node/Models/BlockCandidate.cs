namespace Node.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class BlockCandidate
    {
        public int Index { get; set; }

        public List<ConfirmedTransaction> Transactions { get; set; }

        public int Difficulty { get; set; }

        [JsonProperty("prevBlockHash")]
        public string PreviousBlockHash { get; set; }

        // miner address
        public string MinedBy { get; set; }

        [JsonIgnore]
        public string BlockDataHash { get; set; }

        public string AsJson()
        {
            var jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(this, jsonSettings);
        }
    }
}
