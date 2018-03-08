namespace Node.Models
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Utilities.Json;
    using Utilities.Json.CustomAttributes;

    public class PendingTransaction
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("senderPubKey")]
        public string SenderPublicKey { get; set; }

        [SignatureIgnore]
        [JsonProperty("senderSignature")]
        public string[] SenderSignature { get; set; }

        [HashIgnore]
        [JsonProperty("transactionHash")]
        public string Hash { get; set; }

        public string AsJsonWithoutSignatureAndHash()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new IgnorePropertiesContractResolver(typeof(SignatureIgnore), typeof(HashIgnore))
            };

            return JsonConvert.SerializeObject(this, jsonSettings);
        }

        public string AsJsonWithoutHash()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new IgnorePropertiesContractResolver(typeof(HashIgnore))
            };

            return JsonConvert.SerializeObject(this, jsonSettings);
        }
        public string AsJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
