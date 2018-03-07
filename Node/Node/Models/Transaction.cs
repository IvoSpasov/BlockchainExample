namespace Node.Models
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Utilities.Json;

    public class Transaction
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("senderPubKey")]
        public string SenderPublicKey { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        [MyCustomIgnore]
        [JsonProperty("senderSignature")]
        public string[] SenderSignature { get; set; }

        [JsonIgnore]
        public string Hash { get; set; }

        public string AsJsonStringWithSignature()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string AsJsonStringWithoutSignature()
        {
            var jsonSettings = new JsonSerializerSettings { ContractResolver = new IgnorePropertyContractResolver() };
            return JsonConvert.SerializeObject(this, jsonSettings);
        }
    }
}
